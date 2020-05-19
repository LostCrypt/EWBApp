using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        //Mouse
        public MouseState newState;
        public MouseState oldState;
        Rectangle mouseDetect;
        Rectangle mouseHover;

        //Buttons
        public Texture2D testButtonImg;

        Button testButton;

        public Game1()
        {
            //Viewport
            graphics = new GraphicsDeviceManager(this);

            //Pipeline
            Content.RootDirectory = "Content";

            //Screen Size - 1080 x 1920
            graphics.PreferredBackBufferWidth = 562;
            graphics.PreferredBackBufferHeight = 1000;
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

            //MOUSE//

            this.IsMouseVisible = true; //False by default

            mouseDetect.Width = 1;
            mouseDetect.Height = 1;

            mouseHover.Width = 1;
            mouseHover.Height = 1;

            //BUTTONS//

            testButtonImg = Content.Load<Texture2D>("Images/testImg");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Fonts
            mainFont = Content.Load<SpriteFont>("Fonts/mainFont");

            //Buttons

            testButton = new Button("testing", new Rectangle(screen.Width / 2, 500, 250, 250), testButtonImg);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseInput();

            base.Update(gameTime);
        }

        void MouseInput()
        {
            newState = Mouse.GetState();

            //Click
            if (newState.LeftButton == ButtonState.Pressed && 
                oldState.LeftButton == ButtonState.Released)
            {
                mouseDetect.X = newState.X;
                mouseDetect.Y = newState.Y;
            }

            //Hover
            mouseHover.X = newState.X;
            mouseHover.Y = newState.Y;

            //Assigning old state for next use
            oldState = newState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //Debug Menu
            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                spriteBatch.DrawString(mainFont, "= Debug Menu =", new Vector2(5, 10), Color.Red);
                spriteBatch.DrawString(mainFont, "Mouse Location: " + mouseHover.ToString(), new Vector2(5, 45), Color.Red);
                spriteBatch.DrawString(mainFont, "Mouse Hitbox: " + mouseDetect.ToString(), new Vector2(5, 65), Color.Red);
                spriteBatch.DrawString(mainFont, "Mouse Click: " + oldState.LeftButton.ToString(), new Vector2(5, 85), Color.Red);
            }

            testButton.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
