using Asteroid.Physic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    public class Player : GameEntity
    {
        public const float width = 62;
        public const float height = 62;

        private int health;

        public Player(float x, float y) : base(Assets.getTexture("Graphics/gravel"), x, y, width, height)
        {
            this.health = 3;
        }

        public override void update(float delta)
        {
            base.update(delta);

            //update player
            Physics.processCollision(this);
        }

        public override void collide(GameEntity entity)
        {
            if (entity == null)
            {
                // Outside call
            }
            else if(entity.GetType() == typeof(AsteroidEntity))
            {
                subtractHealth();
                entity.kill();
                //todo checkIfDead(); 
             }
        }

        private void checkIfDead()
        {
            if (health <= 0)
            {
                kill();
            }
        }

        public void subtractHealth()
        {
            health -=1;
        }

        public void addHealth()
        {
            health++;
        }
    }
}
