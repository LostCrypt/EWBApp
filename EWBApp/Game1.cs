using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace EWBApp
{
    public class Game1 : Game
    {
        //Graphics
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //The screen
        Rectangle screen;

        //Fonts
        private SpriteFont mainFont;

        public Game1()
        {
            //Viewport
            graphics = new GraphicsDeviceManager(this);

            //Pipeline
            Content.RootDirectory = "Content";

            //Screen Size - 1080 x 1920
            graphics.PreferredBackBufferWidth = 375;
            graphics.PreferredBackBufferHeight = 667;
        }

        protected override void Initialize()
        {
            //SCREEN//

            //Screen Dimensions
            screen.Width = GraphicsDevice.Viewport.Width;
            screen.Height = GraphicsDevice.Viewport.Height;

            //Screen Position
            screen.X = 0;
            screen.Y = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Fonts
            mainFont = Content.Load<SpriteFont>("Fonts/mainFont");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //Debug Menu
            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                spriteBatch.DrawString(mainFont, "This is the Debug Menu!", new Vector2(20, 20), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
