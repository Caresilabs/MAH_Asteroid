using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid
{
    public class FXManager
    {
        private FXPool pool;
        private List<Particle> particles;

        private float particleSpeed = 600;

        public FXManager()
        {
            this.pool = new FXPool();
            this.particles = new List<Particle>();
        }

        public void explosion(float x, float y)
        {
            for (int i = 0; i < 60; i++)
            {
                Particle p = pool.GetObject();
                p.set(x, y, MathUtils.random(-particleSpeed, particleSpeed),
                    MathUtils.random(-particleSpeed, particleSpeed));
                particles.Add(p);
            }
        }

        public void playerExplosion(Vector2 position)
        {
            for (int i = 0; i < 200; i++)
            {
                Particle p = pool.GetObject();
                p.set(position.X, position.Y, MathUtils.random(-particleSpeed, particleSpeed),
                    MathUtils.random(-particleSpeed, particleSpeed));
                p.setColor(Color.Red);
                particles.Add(p);
            }
        }

        public void playerHit(float x, float y)
        {
            for (int i = 0; i < 120; i++)
            {
                Particle p = pool.GetObject();
                p.set(x, y, MathUtils.random(-particleSpeed, particleSpeed),
                    MathUtils.random(-particleSpeed, particleSpeed));
                p.setColor(Color.Red);
                particles.Add(p);
            }
        }

        public void update(float delta)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                Particle item = particles[i];
                item.update(delta);
                if (! item.isParticleAlive())
                {
                    pool.ReleaseObject(item);
                    particles.Remove(item);
                }
            }
        }

        public void draw(SpriteBatch batch) 
        {
            foreach (var item in particles)
            {
                item.draw(batch);
            }
        }
    }
}
