using Asteroid.Physic;
using Asteroid.Tools;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    public class AsteroidEntity : GameEntity
    {
        public const float widthSmall = 40;
        public const float heightSmall = 40;

        public const float widthBig = 100;
        public const float heightBig = 100;

        private Type type;

        public enum Type
        {
            BIG, SMALL
        }

        public AsteroidEntity(Texture2D texture, Type type, float x, float y) : base(texture, x, y, 0, 0)
        {
            this.type = type;
            if (type == Type.BIG)
            {
                setSize(widthBig, heightBig);
            }
            else if (type == Type.SMALL)
            {
                setSize(widthSmall, heightSmall);
            }
            setMaxSpeed(200);
            setFriction(0);

            setVelocity(MathUtils.random(getMaxSpeed()), MathUtils.random(getMaxSpeed()));
        }

        public override void update(float delta)
        {
            base.update(delta);

            Physics.processCollision(this);
        }

        public override void collide(GameEntity entity)
        {
            if (entity == null)
            {
                // out of bounds
            }
            else if (entity.GetType() == typeof(Bullet))
            {
                if (type == Type.BIG)
                {
                    world.spawnAsteroid(Type.SMALL, getPosition());
                    entity.kill();
                    kill();
                }
                else if (type == Type.SMALL)
                {
                    entity.kill();
                    kill();
                }
            }
        }
    }
}
