using Asteroid.Physic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    public class Player : GameEntity
    {
        public const float width = 48;
        public const float height = 48;

        private Rectangle safeZoneBounds;

        private int health;

        public Player(float x, float y) : base(Assets.getTexture("Graphics/player"), x, y, width, height)
        {
            this.safeZoneBounds = new Rectangle();
            this.health = 3;
        }

        public override void update(float delta)
        {
            base.update(delta);

            // update safe zone
            safeZoneBounds.X = (int)(getBounds().X - Player.width * 3);
            safeZoneBounds.Y = (int)(getBounds().Y - Player.height * 3);
            safeZoneBounds.Width = (int)(getBounds().Width + Player.width * 3);
            safeZoneBounds.Height = (int)(getBounds().Height + Player.height * 3);

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
                world.getEffects().playerHit(getPosition().X, getPosition().Y);
                world.getEffects().explosion(entity.getPosition().X + entity.getBounds().Width / 2, entity.getPosition().Y + entity.getBounds().Height / 2);

                //  setVelocity(entity.getVelocity().X *7, entity.getVelocity().Y *7); //todo
                checkIfDead(); 
             }
        }

        private void checkIfDead()
        {
            if (health <= 0)
            {
                world.getEffects().playerExplosion(getPosition());
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

        public int getHealth()
        {
            return health;
        }

        public Rectangle getSafeZoneBounds()
        {
            return safeZoneBounds;
        }
    }
}
