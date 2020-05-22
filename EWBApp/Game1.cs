﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EWBApp
{
    public enum ScreenStates
    {
        Welcome,
        Signin,
        Visitor,
        Ranger
    }
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

        //States
        ScreenStates screenState;

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

            //STATES//
            screenState = ScreenStates.Welcome;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Fonts
            mainFont = Content.Load<SpriteFont>("Fonts/mainFont");

            //Buttons

            testButton = new Button("testing", new Rectangle(screen.Width / 2, screen.Height / 2, 250, 250), testButtonImg);
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

            DragDrop(testButton);

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

        void DragDrop(Button button)
        {
            if (button.HasCollided(mouseDetect) && button.flag == false)
            {
                mouseDetect.X = -1000;
                mouseDetect.Y = -1000;
                button.flag = true;
            }
            
            if (button.HasCollided(mouseDetect) && button.flag == true)
            {
                mouseDetect.X = -1000;
                mouseDetect.Y = -1000;
                button.flag = false;
            }

            if (button.flag == true)
            {
                button.bounds.X = mouseHover.X - button.sprite.Width / 2;
                button.bounds.Y = mouseHover.Y - button.sprite.Height / 2;
            }
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
                spriteBatch.DrawString(mainFont, "Test Collision: " + testButton.HasCollided(mouseDetect).ToString(), new Vector2(5, 105), Color.Red);
            }

            testButton.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
