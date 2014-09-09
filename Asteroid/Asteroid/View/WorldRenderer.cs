using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Model;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid.View
{
    public class WorldRenderer
    {
        private World world;

        public WorldRenderer(World world)
        {
            this.world = world;
        }

        public void render(SpriteBatch batch)
        {
            // todo render game
            foreach (GameEntity entity in world.getEntities())
            {
                entity.draw(batch);
            }
        }
    }
}
