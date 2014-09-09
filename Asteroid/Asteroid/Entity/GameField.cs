using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    /// <summary>
    /// This class manage the game field where player are allowed to be in
    /// </summary>
    public class GameField
    {
        private Rectangle bounds;
        private Texture2D boundsTexture;

        private int thickness = 10;

        public enum FieldHit {
            Left, Right, Top, Bottom, Inside
        }

        public GameField(int width, int height)
        {
            this.bounds = new Rectangle(-width / 2, -height / 2, width, height);
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
            if (e.getPosition().X < bounds.X + e.getBounds().Width/2) return FieldHit.Left;

            if (e.getPosition().X > bounds.X + bounds.Width - e.getBounds().Width/2) return FieldHit.Right;

            if (e.getPosition().Y > bounds.Y + bounds.Height - e.getBounds().Height / 2) return FieldHit.Bottom;

            if (e.getPosition().Y < bounds.Y + e.getBounds().Height/2) return FieldHit.Top;

            return FieldHit.Inside;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }
    }
}
