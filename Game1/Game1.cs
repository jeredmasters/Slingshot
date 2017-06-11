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
        SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Animal> animals;
        Breeder Breeder;
        Statistics Stats;
        GenerationStat CurrentGeneration;
        bool firstClick = true;
        Vector2 Gravity = new Vector2(0, Utility.Scale(0.05));
        bool activeGravity = true;
        KeyboardState previousState;

        int rate = 5;
        const int maxY = 1000;
        const int maxX = 1800;
        const int floor = maxY - 100;
        const int startline = 200;
        
        Vector2 Starting = new Vector2(startline, floor);
        DateTime lastGeneration;
        Population population;
        

        public Game1()
        {
            Stats = new Statistics();
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
            
            populate();
            previousState = Keyboard.GetState();
            base.Initialize();
        }

        private void populate()
        {
            if (CurrentGeneration != null)
            {
                //Stats.GenStat.Add(CurrentGeneration);
                
            }
            animals = new List<Animal>();
            population = Breeder.getNextGeneration();
            CurrentGeneration = new GenerationStat(Stats.GenStat.Count, population.Size);
            firstClick = true;
            foreach (Gene gene in population.Genes)
            {
                animals.Add(new Animal(gene, GraphicsDevice, Starting));
            }
            lastGeneration = DateTime.Now;
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
            font = Content.Load<SpriteFont>("Control");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        Random rnd = new Random();
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ProcessInput();
            if ((DateTime.Now - lastGeneration).TotalSeconds >= (30 / rate))
            {
                populate();
            }
            for(int i = 0; i < rate; i++)
            {
                ProcessPhysics();
                ProcessInput();
            }

            base.Update(gameTime);
        }
        private void ProcessInput()
        {
            KeyboardState state = Keyboard.GetState();

            // Move our sprite based on arrow keys being pressed:

            if (state.IsKeyDown(Keys.Up) && !previousState.IsKeyDown(Keys.Up))
                rate++;
            if (state.IsKeyDown(Keys.Down) && !previousState.IsKeyDown(Keys.Down))
                rate--;
            if (state.IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space))
                activeGravity = !activeGravity;

            if (rate < 1)
            {
                rate = 1;
            }
            if (rate > 30)
            {
                rate = 30;
            }
            previousState = Keyboard.GetState();
        }
        private void DrawStats()
        {
            spriteBatch.DrawString(font, "Rate: " + rate, new Vector2(50, 50), Color.Black);
        }
        private void ProcessPhysics()
        {
            foreach (var animal in animals)
            {
                animal.Fitness = 0;// rnd.Next(0, 255);
                bool onGround = false;
                foreach (var m in animal.Muscles)
                {                    
                    m.PosC = animal.Nodes[m.NodeC].Position;
                    m.PosP = animal.Nodes[m.NodeP].Position;
                    if (m.OscRange > 0)
                    {
                        m.OscState += (m.OscDirection ? -1 : 1) * m.OscSpeed;
                        if (m.OscState >= m.OscRange)
                        {
                            m.OscState = m.OscRange;
                            m.OscDirection = !m.OscDirection;
                        }
                        if (m.OscState <= 0)
                        {
                            m.OscState = 0;
                            m.OscDirection = !m.OscDirection;
                        }
                        m.Length = m.LengthAlpha + m.OscState;
                    }
                }
                foreach (Node node in animal.Nodes)
                {
                    Vector2 force = FindGravityForce(node);
                    force += FindMuscleForce(node);
                    force += FindFrictionForce(node);
                    node.Speed = node.Speed * (float)0.99;
                    node.Speed += force / node.Weight;
                    node.Position += node.Speed * 2;

                    if (node.Position.X <= 1)
                    {
                        node.Speed.X = (float)Math.Abs(node.Speed.X * 0.5);
                        node.Position.X = 5;
                    }
                    if (node.Position.X >= maxX)
                    {
                        node.Speed.X = (float)Math.Abs(node.Speed.X * 0.5) * -1;
                        node.Position.X = maxX - 5;
                    }
                    if (node.Position.Y <= 0)
                    {
                        node.Speed.Y = (float)Math.Abs(node.Speed.Y * 0.5);
                        node.Position.Y = 5;
                    }
                    if (node.Position.Y >= floor)
                    {
                        node.Position.Y = floor;
                        if (node.Speed.Y > 0)
                        {
                            node.Speed.Y = 0;
                        }
                        node.Speed.X = (float)(node.Speed.X / (Math.Sqrt(node.Weight) / 5 + 1));
                        onGround = true;

                    }

                    //if (node.Position.Y < floor)
                    //{
                    //    animal.Fitness += (int)(floor - node.Position.Y);
                    //}
                    var fitness = (int)((floor - node.Position.Y) * (node.Position.X - 100));
                    animal.Fitness += fitness;
                }
                if (!onGround)
                {
                    animal.Fitness = 0;
                }
            }
            if (firstClick)
            {
                CurrentGeneration.AvgInitDelta += CurrentGeneration.AvgInitDelta / CurrentGeneration.Size;
                CurrentGeneration.AvgInitForce += CurrentGeneration.AvgInitForce / CurrentGeneration.Size;
            }
            firstClick = false;

        }
        private Vector2 FindGravityForce(Node node)
        {
            return Gravity * node.Weight;            
        }
        private Vector2 FindMuscleForce(Node node)
        {
            Vector2 force = new Vector2(0, 0);
            foreach(var m in node.Muscles)
            {
                var a = m.PosP - m.PosC;
                if (m.NodeC == node.ID)
                {
                    a = m.PosC - m.PosP;
                }
                var distance = a.Length();
                var contract = (m.Length < distance);
                var delta = Math.Abs(distance - (short)m.Length);
                var pressure = Math.Pow(delta / 4, 3);
                var balance = (500000 / m.Strength);
                var relative = pressure / balance;
                    
                var mForce = new Vector2((float)(a.X * relative),(float)(a.Y * relative));
                if (float.IsInfinity(mForce.X) || float.IsInfinity(mForce.Y))
                {
                    mForce = new Vector2(0, 0);
                }
                while (mForce.Length() > 20)
                {
                    mForce = mForce / 10;
                }
                force += (mForce * (contract ? -1 : 1));
                if (firstClick)
                {
                    CurrentGeneration.AvgInitDelta += delta;
                    CurrentGeneration.AvgInitForce += force.Length();
                }                
            }
            return force;
        }
        private Vector2 FindFrictionForce(Node node)
        {
            return node.Speed / -5;
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
            DrawStats();
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
                muscle.Color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
