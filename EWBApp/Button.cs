using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EWBApp
{
    class Button
    {
        //Public - Unrestricted Access
        public String buttonName;
        public bool hasText;
        public SpriteFont bodyFont;

        public Rectangle bounds;
        public bool flag = false;
        public Texture2D sprite;

        //Protected - Class Only Access
        protected Vector2 middle;

        //Constructor
        public Button(String a_buttonName, bool a_hasText, SpriteFont a_bodyFont, Rectangle a_bounds, Texture2D a_sprite)
        {
            buttonName = a_buttonName;
            hasText = a_hasText;
            bodyFont = a_bodyFont;

            bounds = a_bounds;
            sprite = a_sprite;
        }

        public bool HasCollided(Rectangle a_intersect)
        {
            return (bounds.Intersects(a_intersect));
        }

        Vector2 StringAlign()
        {
            //VARIABLES//
            Vector2 size = bodyFont.MeasureString(buttonName);
            Vector2 pos;

            Vector2 origin = size / 2;
            pos.X = bounds.Width / 2;
            pos.Y = bounds.Height / 2;

            //CALCULATIONS//
            Vector2 center = pos - origin;

            center.X += bounds.X;
            center.Y += bounds.Y;

            return center;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(sprite, bounds, Color.White);

            if (hasText)
            {
                middle = StringAlign();
                spriteBatch.DrawString(bodyFont, buttonName, middle, Color.Black);
            }
        }
    }
}
