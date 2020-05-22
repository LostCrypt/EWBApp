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
        public Rectangle bounds;
        public bool flag = false;
        public Texture2D sprite;

        //Protected - Class Only Access


        //Constructor
        public Button(String a_buttonName, Rectangle a_bounds, Texture2D a_sprite)
        {
            buttonName = a_buttonName;

            bounds = a_bounds;
            sprite = a_sprite;
        }

        public bool HasCollided(Rectangle a_intersect)
        {
            return (bounds.Intersects(a_intersect));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(sprite, bounds, Color.White);
        }
    }
}
