using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroid.Tools;

namespace Asteroid.Entity
{
    public class Particle : IPoolable
    {
        private Vector2 position;
        private Vector2 velocity;
        private Color color;

        private Texture2D texture;

        private bool isAlive;
        private float maxAliveTime = 1.1f;
        private float aliveTime;

        public Particle()
        {
            this.texture = Assets.getTexture("Graphics/pixel");
            this.color = Color.Gray;
            this.isAlive = true;
            this.position = new Vector2();
            this.velocity = new Vector2();
        }

        public void reset()
        {
            aliveTime = 0;
            setColor(Color.Gray);
        }

        public void set(float x, float y, float vx, float vy)
        {
            this.isAlive = true;

            this.position.X = x;
            this.position.Y = y;

            this.velocity.X = vx;
            this.velocity.Y = vy;
        }

        public void update(float delta)
        {
            position.X += velocity.X * delta;
            position.Y += velocity.Y * delta;

            aliveTime += delta;
            if (aliveTime > maxAliveTime)
            {
                isAlive = false;
            }
        }

        public void draw(SpriteBatch batch)
        {
            if (isAlive)
                batch.Draw(texture, position, color);
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public bool isParticleAlive()
        {
            return isAlive;
        }
    }
}
