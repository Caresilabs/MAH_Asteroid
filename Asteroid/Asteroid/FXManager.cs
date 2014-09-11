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

        public FXManager()
        {
            this.pool = new FXPool();
            this.particles = new List<Particle>();
        }

        public void explosion(Vector2 position)
        {
            for (int i = 0; i < 100; i++)
            {
                Particle p = pool.GetObject();
                p.set(MathUtils.random(100), MathUtils.random(100), new Vector2(MathUtils.random(100), MathUtils.random(100)));
                particles.Add(p);
            }
        }

        public void update(float delta)
        {
            foreach (var item in particles)
            {
                item.update(delta);
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
