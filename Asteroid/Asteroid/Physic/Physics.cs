using Asteroid.Entity;
using Asteroid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Physic
{
    public class Physics
    {
        private static World world;

        public static void init(World world) {
            Physics.world = world;
        }

        public static void processCollision(GameEntity entity)
        {
            // Iterate and check for possible collides
            foreach (GameEntity ent in world.getEntities()) {
                if (ent == entity) continue; // Cant collide with itsef

                if (entity.getBounds().Intersects(ent.getBounds())) {
                    entity.setPosition(ent.getBounds().X - ent.getBounds().Width / 2 - entity.getBounds().Width/2,
                        ent.getBounds().Y - ent.getBounds().Height / 2 - entity.getBounds().Height/2); //todo bad code

                    entity.flipVelocity();
                    ent.flipVelocity();

                    entity.collide(ent);
                    ent.collide(entity);
                }
            }
        }
    }
}
