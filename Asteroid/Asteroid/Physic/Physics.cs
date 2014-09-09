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

        public static void init(World world)
        {
            Physics.world = world;
        }

        public static void processCollision(GameEntity entity)
        {
            // Iterate and check for possible collides
            foreach (GameEntity ent in world.getEntities())
            {
                if (ent == entity) continue; // Cant collide with itsef
                if (!ent.isEntityAlive()) continue;

                // Check if it is a bullet and the spawner cannot collide with it
                if (entity.GetType() == typeof(Bullet))
                {
                    if (((Bullet)entity).getSource() == ent) continue;
                }
                else if (ent.GetType() == typeof(Bullet))
                {
                    if (((Bullet)ent).getSource() == entity) continue;
                }

                if (entity.getBounds().Intersects(ent.getBounds()))
                {

                    float x = (entity.getPosition().X + (entity.getBounds().Width / 2)) - (ent.getPosition().X + (ent.getBounds().Width / 2));
                    float y = (entity.getPosition().Y + (entity.getBounds().Height / 2)) - (ent.getPosition().Y + (ent.getBounds().Height / 2));

                    if (entity.GetType() != typeof(Bullet) && ent.GetType() != typeof(Bullet))
                        if (Math.Abs(x) > Math.Abs(y))
                        {
                            // reflect horizontally
                            entity.flipVelocityX();
                            //.addVelocityStep();

                            ent.flipVelocityX();
                            // ent.addVelocityStep();

                            //entity.setPosition(entity.getPosition().X + y/2, entity.getPosition().Y + y/2);
                        }
                        else
                        {
                            entity.flipVelocityY();
                            //entity.addVelocityStep();

                            ent.flipVelocityY();
                            //ent.addVelocityStep();
                            // reflect vertically
                        }

                    handleCollision(entity, ent);
                }

            }
        }

        private static void handleCollision(GameEntity e1, GameEntity e2)
        {
            e1.collide(e2);
            e2.collide(e1);
        }
    }
}
