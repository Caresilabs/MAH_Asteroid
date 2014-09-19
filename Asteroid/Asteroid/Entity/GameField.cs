using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    /**
     *  This class manage the game field where player are allowed to be in
     */
    public class GameField
    {
        public static Rectangle bounds;
        public static int thickness = 10;

        private Texture2D boundsTexture;

        public enum FieldHit {
            Left, Right, Top, Bottom, Inside
        }

        public GameField(int width, int height)
        {
            bounds = new Rectangle(-width / 2, -height / 2, width, height);
            this.boundsTexture = Assets.getTexture("Graphics/pixel");
        }

        public void draw(SpriteBatch batch)
        {
            // top
            batch.Draw(boundsTexture, new Rectangle(bounds.X, bounds.Y, bounds.Width, thickness), Color.White);

            // right
            batch.Draw(boundsTexture, new Rectangle(bounds.X + bounds.Width, bounds.Y, thickness, bounds.Height), Color.White);

            // left
            batch.Draw(boundsTexture, new Rectangle(bounds.X, bounds.Y, thickness, bounds.Height), Color.White);

            // bot
            batch.Draw(boundsTexture, new Rectangle(bounds.X, bounds.Y + bounds.Height, bounds.Width + thickness, thickness), Color.White);
        }

        public FieldHit checkInside(GameEntity e)
        {
            if (e.getPosition().X < bounds.X + thickness) return FieldHit.Left;

            if (e.getPosition().X > bounds.X + bounds.Width - e.getBounds().Width) return FieldHit.Right;

            if (e.getPosition().Y > bounds.Y + bounds.Height - e.getBounds().Height) return FieldHit.Bottom;

            if (e.getPosition().Y - thickness < bounds.Y) return FieldHit.Top;

            return FieldHit.Inside;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }
    }
}
