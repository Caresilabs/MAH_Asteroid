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

        public enum FieldHit {
            Left, Right, Top, Bottom, Inside
        }

        public GameField(int width, int height)
        {
            this.bounds = new Rectangle(-width / 2, -height / 2, width, height);
            this.boundsTexture = Assets.getTexture("Graphics/gravel");
        }

        public void draw(SpriteBatch batch)
        {
            //batch.Draw(boundsTexture, );
        }

        public FieldHit checkInside(GameEntity e)
        {
            if (e.getPosition().X < bounds.Left) return FieldHit.Left;

            if (e.getPosition().X > bounds.Right) return FieldHit.Right;

            if (e.getPosition().Y > bounds.Top) return FieldHit.Top;

            if (e.getPosition().Y < bounds.Bottom) return FieldHit.Bottom;

            return FieldHit.Inside;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }
    }
}
