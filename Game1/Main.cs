using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Slingshot
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SlingShot : Game
    {
        SpriteFont _font;
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;        
        Species _species;
        
        Statistics _stats;
        Configuration _config;
        GenerationStat _currentGen;
        Input _input;
        Textures _textures;
        Physics _physics;
        IUtility _utility;
        
        
        int rate = 5;

        const int WindowHeight = 1000;
        const int WindowWidth = 1800;
        const int Floor = WindowHeight - 100;
        const int startline = 200;
        
        Vector2 _startingPosition = new Vector2(startline, Floor);
               

        public SlingShot()
        {
            Content.RootDirectory = "Content";

            

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.PreferredBackBufferWidth = WindowWidth;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _stats = new Statistics();
            _input = new Input();
            _config = new Configuration();
            _textures = new Textures(GraphicsDevice);
            _species = new Species(_startingPosition, _config.PopulationSize);
            _physics = new Physics(Floor, WindowWidth, WindowHeight, Helper.Scale(0.2));
            _utility = new UtilityWalker(Floor);

            NewGeneration();
            _input.Initialize();           
            base.Initialize();
        }

        public void NewGeneration()
        {
            if (_currentGen != null)
            {
                _stats.GenStat.Add(_currentGen);

            }
            _species.NewGeneration();
            clicks = 0;        
        }



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _font = Content.Load<SpriteFont>("Control");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        int clicks = 0;
        Random rnd = new Random();
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (clicks >= 2000)
            {
                NewGeneration();
            }
            for(int i = 0; i < rate; i++)
            {
                clicks++;
                _physics.ProcessPhysics(_species.Animals);
                _species.Fittest = _utility.Evaluate(_species.Animals);
                _input.ProcessInput();
            }

            base.Update(gameTime);
        }
        
        


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (var animal in _species.Animals)
            {
                DrawAnimal(animal);
            }
            DrawStats();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawAnimal(Animal animal)
        {                
            foreach(Muscle m in animal.Muscles)
            {                                
                DrawLine(m);
            }
            foreach (Node n in animal.Nodes)
            {
                _spriteBatch.Draw(_textures.Node(n.Weight) , n.Position - n.Offest);
            }
        }
        void DrawLine(Muscle muscle)
        {
            Vector2 edge = muscle.PosC - muscle.PosP;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            var texture = _textures.Muscle(muscle.Strength);
            _spriteBatch.Draw(texture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)muscle.PosP.X,
                    (int)muscle.PosP.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                new Color((255 - muscle.Strength), 10, 10), //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
        private void DrawStats()
        {
            string[] data = new string[]
            {
                "Generation: " + _stats.GenStat.Count,
                "Rate: " + rate,
                "Clicks: " + clicks,
                "Fittest: " + (_species.Fittest == null ? "n/a" : _species.Fittest.ID.ToString() + "(" + _species.Fittest.Fitness.ToString() +")"),
                "Leaps: " + _stats.Leaps,
                "Clipping: " + _physics.Clipping
            };
            _spriteBatch.DrawString(_font, string.Join("\n", data), new Vector2(50, 50), Color.Black);
        }
    }
}
