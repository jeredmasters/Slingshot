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

        Input _input;
        Textures _textures;
        Simulation _simulation;
        Configuration _config;
        Storage _storage;
        bool _draw = false;
        int _generations = 20;
        

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
            //ConfigurationWindow window = new ConfigurationWindow();
            //window.ShowDialog();
            _storage = new Storage("localhost", "Slingshot", "sa", "Passw0rd");
            _textures = new Textures(GraphicsDevice);
            _input = new Input();

            _config = new Configuration()
            {
                Complexity = new Incrementer(10,100,20),
                CrossoverRate = new Incrementer(0, 10, 2),
                Duration = new Incrementer(1000, 4000, 1000),
                MutationRate = new Incrementer(10, 200, 50),
                PopulationSize = new Incrementer(100, 300, 100),
                SelectionPressure = new Incrementer(1, 10, 5)
            };

            NewSimulation();

            _input.Initialize();           
            base.Initialize();
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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (_simulation.Generation > _generations)
            {
                NewSimulation();
            }
            _simulation.Click();
            if (ProcessInput())
            {
                this.BeginDraw();
            }

            base.Update(gameTime);
        }


        int _attempt = 0;
        int _simCount = 0;
        protected void NewSimulation()
        {
            _attempt++;
            _simCount++;            
            
            if (_attempt >= 1)
            {
                _attempt = 0;
                _config.Increment();                
            }

            if (_simulation != null)
            {
                _storage.EndSimulation(_simulation.Id, _simulation.Fittest);
            }

            int simId = _storage.NewSimulation(_config, _generations);

            _simulation = new Simulation(simId, _config, (_simulation != null ? _simulation.Rate : 10), _storage);

            
        }

        protected bool ProcessInput()
        {
            var keys = _input.ProcessInput();
            var action = false;
            foreach(var key in keys)
            {
                switch (key)
                {
                    case Keys.Up:
                        _simulation.Rate++;
                        action = true;
                        break;
                    case Keys.Down:
                        _simulation.Rate--;
                        action = true;
                        break;
                    case Keys.Space:
                        _draw = !_draw;
                        break;
                }
            }
            return action;
        }
        
        


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (_simulation.Rate <= 15 && _draw)
            {
                foreach (var animal in _simulation.Animals)
                {
                    DrawAnimal(animal);
                }
            }
            DrawStats(_simulation.getStats(), new Vector2(50, 50));
            DrawStats(_config.getStats(), new Vector2(1000, 50));
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
        private void DrawStats(Dictionary<string,string> data, Vector2 position)
        {
            string labels = "";
            string values = "";
            foreach(var d in data)
            {
                labels += d.Key + "\n";
                values += d.Value + "\n";
            }
            _spriteBatch.DrawString(_font, labels, position, Color.Black);
            _spriteBatch.DrawString(_font, values, position + new Vector2(200,0), Color.Black);
        }
    }
}
