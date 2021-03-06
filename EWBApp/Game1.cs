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

        public Texture2D map;

        public Texture2D logo;

        //Fonts
        private SpriteFont mainFont;
        private SpriteFont titleFont;
        private SpriteFont headingFont;
        private SpriteFont subHeadingFont;
        public SpriteFont bodyFont;

        //Mouse
        public MouseState newState;
        public MouseState oldState;
        Rectangle mouseDetect;
        Rectangle mouseHover;

        //Buttons
        public Texture2D testButtonImg;
        public Texture2D pinImg;
        public Texture2D gasImg;
        public Texture2D foodImg;
        public Texture2D buttonFull;
        public Texture2D buttonEmpty;
        public Texture2D buttonMenuImg;
        public Texture2D buttonBgImg;

        //Normal
        Button loginButton;
        Button rangerSign;
        Button visitorSign;
        Button menuButton;

        //DragDrop
        Button testButton;
        Button poi1;
        Button poi2;
        Button gas;
        Button food;

        List<Button> iconList;

        List<Button> signButtonList;

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

            MouseReset();

            mouseHover.Width = 1;
            mouseHover.Height = 1;

            //STATES//
            screenState = ScreenStates.Welcome;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Normal Buttons
            buttonFull = Content.Load<Texture2D>("Images/buttonFull");
            buttonEmpty = Content.Load<Texture2D>("Images/buttonEmpty");
            buttonMenuImg = Content.Load<Texture2D>("Images/menuButton");

            //DragDrop Buttons
            testButtonImg = Content.Load<Texture2D>("Images/testImg");
            pinImg = Content.Load<Texture2D>("Images/pin");
            gasImg = Content.Load<Texture2D>("Images/gasIcon");
            foodImg = Content.Load<Texture2D>("Images/foodIcon");

            //Fonts
            titleFont = Content.Load<SpriteFont>("Fonts/titleFont");
            mainFont = Content.Load<SpriteFont>("Fonts/mainFont");
            headingFont = Content.Load<SpriteFont>("Fonts/headingFont");
            subHeadingFont = Content.Load<SpriteFont>("Fonts/subHeadingFont");
            bodyFont = Content.Load<SpriteFont>("Fonts/bodyFont");

            //Background
            map = Content.Load<Texture2D>("Images/mapbg");
            logo = Content.Load<Texture2D>("Images/logo");

            //BUTTONS//
            iconList = new List<Button>();
            signButtonList = new List<Button>();

            //Sign-In screen
            loginButton = new Button("Login", true, bodyFont, new Rectangle(screen.Width / 2 - buttonFull.Width / 2, 700, buttonFull.Width, buttonFull.Height), buttonFull);
            rangerSign = new Button("Ranger", true, bodyFont, new Rectangle(80, 400, buttonEmpty.Width, buttonEmpty.Height), buttonEmpty);
            visitorSign = new Button("Visitor", true, bodyFont, new Rectangle(286, 400, buttonEmpty.Width, buttonEmpty.Height), buttonEmpty);

            // Ranger Screen
            menuButton = new Button("Menu", false, bodyFont, new Rectangle(screen.Width - 60, screen.Height - 60, 50, 50), buttonMenuImg);
            testButton = new Button("testing", false, bodyFont, new Rectangle(screen.Width / 2, screen.Height / 2, 250, 250), testButtonImg);
            poi1 = new Button("poi1", false, bodyFont, new Rectangle(screen.Width + 20, 450, 50, 50), pinImg);
            poi2 = new Button("poi2", false, bodyFont, new Rectangle(screen.Width + 20, 500, 50, 50), pinImg);
            gas = new Button("gas", false, bodyFont, new Rectangle(screen.Width + 20, 550, 45, 45), gasImg);
            food = new Button("food", false, bodyFont, new Rectangle(screen.Width + 20, 600, 45, 45), foodImg);

            signButtonList.Add(loginButton);
            signButtonList.Add(rangerSign);
            signButtonList.Add(visitorSign);

            //iconList.Add(testButton);
            iconList.Add(poi1);
            iconList.Add(poi2);
            iconList.Add(gas);
            iconList.Add(food);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseInput();

            if (screenState == ScreenStates.Welcome)
            {
                if (screen.Intersects(mouseDetect))
                {
                    MouseReset();
                    screenState = ScreenStates.Signin;
                }
            }

            if (screenState == ScreenStates.Signin)
            {
                if (loginButton.HasCollided(mouseDetect))
                {
                    MouseReset();
                    screenState = ScreenStates.Ranger;
                }
            }

            if (screenState == ScreenStates.Ranger)
            {
                if (menuButton.HasCollided(mouseDetect) && menuButton.flag == false)
                {
                    MouseReset();
                    menuButton.flag = true;
                }

                if (menuButton.HasCollided(mouseDetect) && menuButton.flag == true)
                {
                    MouseReset();
                    menuButton.flag = false;
                }

                foreach (Button b in iconList)
                {
                    DragDrop(b);

                    if (menuButton.flag == true && b.bounds.X > screen.Width)
                    {
                        b.bounds.X = screen.Width - 60;
                    }

                    if (menuButton.flag == false && b.bounds.X < screen.Width)
                    {
                        b.bounds.X = 563;
                    }
                }
            }

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

        void MouseReset()
        {
            mouseDetect.X = -1000;
            mouseDetect.Y = -1000;
        }

        void DragDrop(Button b)
        {
            if (b.HasCollided(mouseDetect) && b.flag == false)
            {
                MouseReset();
                b.flag = true;
            }
            
            if (b.HasCollided(mouseDetect) && b.flag == true)
            {
                mouseDetect.X = -1000;
                mouseDetect.Y = -1000;
                b.flag = false;
            }

            if (b.flag == true)
            {
                b.bounds.X = mouseHover.X - b.bounds.Width / 2;
                b.bounds.Y = mouseHover.Y - b.bounds.Height / 2;
            }
        }

        Vector2 StringAlign(SpriteFont font, string text, int valueX, int valueY)
        {
            //VARIABLES//
            Vector2 size = font.MeasureString(text);
            Vector2 pos;
            Vector2 origin = size / 2;

            pos.X = screen.Width / 2;
            pos.Y = screen.Height / 2;

            //CALCULATIONS//
            Vector2 center = pos - origin;

            center.X += valueX;
            center.Y += valueY;

            return center;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            if (screenState == ScreenStates.Welcome)
            {
                GraphicsDevice.Clear(Color.FromNonPremultiplied(244, 67, 54, 255));

                spriteBatch.DrawString(titleFont, "Welcome!", StringAlign(titleFont, "Welcome!", 0, 0), Color.White);

                spriteBatch.Draw(logo, new Rectangle(screen.Width / 2 - (logo.Width / 2) - 50, 650, logo.Width + 100, logo.Height + 100), Color.White);
            }

            if (screenState == ScreenStates.Signin)
            {
                GraphicsDevice.Clear(Color.White);

                spriteBatch.DrawString(headingFont, "Sign-Up With Us", new Vector2(40, 40), Color.Black);
                spriteBatch.DrawString(subHeadingFont, "I am a...", new Vector2(80, 300), Color.Black);
                spriteBatch.DrawString(subHeadingFont, "Coming back?", new Vector2(80, 600), Color.Black);

                foreach (Button b in signButtonList)
                {
                    b.Draw(spriteBatch, gameTime);
                }
            }

            if (screenState == ScreenStates.Ranger)
            {
                spriteBatch.Draw(map, new Rectangle(0, 0, screen.Width, screen.Height), Color.White);
                menuButton.Draw(spriteBatch, gameTime);
                foreach (Button b in iconList)
                {
                    b.Draw(spriteBatch, gameTime);
                }
            }

            //Debug Menu
            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                spriteBatch.DrawString(mainFont, "= Debug Menu =", new Vector2(5, 10), Color.Red);
                spriteBatch.DrawString(mainFont, "Mouse Location: " + mouseHover.ToString(), new Vector2(5, 45), Color.Red);
                spriteBatch.DrawString(mainFont, "Mouse Hitbox: " + mouseDetect.ToString(), new Vector2(5, 65), Color.Red);
                spriteBatch.DrawString(mainFont, "Mouse Click: " + oldState.LeftButton.ToString(), new Vector2(5, 85), Color.Red);
                spriteBatch.DrawString(mainFont, "Test Collision: " + testButton.HasCollided(mouseDetect).ToString(), new Vector2(5, 105), Color.Red);
                spriteBatch.DrawString(mainFont, "MenuButton State: " + menuButton.flag.ToString(), new Vector2(5, 125), Color.Red);
                spriteBatch.DrawString(mainFont, "POI1 Location: " + poi1.bounds.X.ToString(), new Vector2(5, 145), Color.Red);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
