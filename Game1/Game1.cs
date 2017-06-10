using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Animal> animals;
        Breeder Breeder;
        
        Vector2 Gravity = new Vector2(0, (float)0.05);

        const int maxY = 1500;
        const int maxX = 3000;
        const int floor = maxY - 100;
        const int startline = 300;
        
        Vector2 Starting = new Vector2(startline, floor);
        DateTime startTime;
        Population population;

        public Game1()
        {
            Breeder = new Breeder();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = maxY;
            graphics.PreferredBackBufferWidth = maxX;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            startTime = DateTime.Now;
            populate();
            base.Initialize();
        }

        private void populate()
        {
            animals = new List<Animal>();
            population = Breeder.getNextGeneration();
            foreach (Gene gene in population.Genes)
            {
                animals.Add(new Animal(gene, GraphicsDevice, Starting));
            }
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if ((int)(DateTime.Now - startTime).TotalSeconds % 15 == 14)
            {
                populate();
            }
            foreach (var animal in animals)
            {
                animal.Fitness = 0;
                bool onGround = false;
                foreach (Node node in animal.Nodes)
                {
                    ApplyGravity(node);
                    ApplyMuscles(node, animal);
                    ApplyFriction(node);
                    node.Position += node.Speed;

                    if (node.Position.X <= 0)
                    {
                        node.Speed.X = (float)Math.Abs(node.Speed.X * 0.5);
                    }
                    if (node.Position.X >= maxX)
                    {
                        node.Speed.X = (float)Math.Abs(node.Speed.X * 0.5) * -1;
                    }
                    if (node.Position.Y <= 0)
                    {
                        node.Speed.Y = (float)(node.Speed.Y * -0.5);
                    }
                    if (node.Position.Y >= floor)
                    {
                        node.Position.Y = floor;
                        if (node.Speed.Y > 0)
                        {
                            node.Speed.Y = 0;
                        }
                        node.Speed.X = (node.Speed.X * 3) / node.Weight;
                        onGround = true;
                        
                    }

                    //if (node.Position.Y < floor)
                    //{
                    //    animal.Fitness += (int)(floor - node.Position.Y);
                    //}
                    if (node.Position.X > animal.Fitness)
                    {
                        animal.Fitness = (int)(node.Position.X * (floor - node.Position.Y));
                    }
                }
                if (!onGround)
                {
                    //animal.Fitness = 0;
                }
            }
            
            base.Update(gameTime);
        }

        private void ApplyGravity(Node node)
        {
            node.Speed += Gravity;
            
        }
        private void ApplyMuscles(Node node, Animal animal)
        {
            foreach(var m in animal.Muscles)
            {                
                if (m.NodeC == node.ID || m.NodeP == node.ID)
                {
                    var a = m.PosP - m.PosC;
                    if (m.NodeC == node.ID)
                    {
                        a = m.PosC - m.PosP;
                    }
                    if (m.Length == null)
                    {
                        m.Length = (short)((a.Length() * 2 + m.LengthAlpha) / 3);
                    }
                    var contract = (m.Length < a.Length());
                    var delta = Math.Abs(a.Length() - (short)m.Length) / 4;
                    var pressure = Math.Pow(delta, 3);
                    var balance = ((50000000 / m.Strength) * node.Weight);
                    var relative = pressure / balance;
                    //a.Normalize();
                    var force = new Vector2((float)(a.X * relative),(float)(a.Y * relative));
                    while (force.Length() > 1)
                    {
                        force = force / 10;
                    }
                    node.Speed += force * (contract ? -1 : 1);
                }
            }
        }
        private void ApplyFriction(Node node)
        {
            node.Speed = (node.Speed * 100) / 101;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (var animal in animals)
            {
                DrawAnimal(animal);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawAnimal(Animal animal)
        {
            foreach(Node node in animal.Nodes)
            {
                spriteBatch.Draw(node.Texture, node.Position);
            }            
            foreach(Muscle m in animal.Muscles)
            {
                m.PosC = animal.Nodes[m.NodeC].Position;
                m.PosP = animal.Nodes[m.NodeP].Position;                
                DrawLine(m);
            }
        }
        void DrawLine(Muscle muscle)
        {
            Vector2 edge = muscle.PosC - muscle.PosP;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);


            spriteBatch.Draw(muscle.Texture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)muscle.PosP.X,
                    (int)muscle.PosP.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
