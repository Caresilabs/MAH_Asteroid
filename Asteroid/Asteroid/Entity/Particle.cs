using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid.Entity
{
    public class Particle
    {
        private Vector2 position;
        private Vector2 velocity;
        private Color color;

        private Texture2D texture;

        public Particle()
        {
            this.texture = Assets.getTexture("Graphics/pixel");
            this.color = Color.Red;
        }

        public void set(float x, float y, Vector2 velocity)
        {
            this.position = new Vector2(x, y);
            this.velocity = velocity;
        }

        public void update(float delta)
        {
            position.X += velocity.X * delta;
            position.Y += velocity.Y * delta;
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, color);
        }
    }
}
