using Asteroid.Physic;
using Asteroid.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Entity
{
    public class Bullet : GameEntity, IPoolable
    {
        public static int bulletSpeed = 600;

        private static int bulletSize = 10;
        private GameEntity source;

        public Bullet()
            : base(Assets.getTexture("Graphics/pixel"), 0, 0, bulletSize, bulletSize)
        {

        }

        public override void update(float delta)
        {
            base.update(delta);
            Physics.processCollision(this);
        }

        public void shoot(GameEntity source, Vector2 pos, Vector2 dir)
        {
            this.source = source;
            setMaxSpeed(bulletSpeed);
            setPosition(pos.X, pos.Y);
            setVelocity(dir.X, dir.Y);
            setFriction(0);
        }

        public void reset()
        {
            setPosition(-1000, -1000);
            setAcceleration(0, 0);
            setVelocity(0, 0);
        }

        public override void collide(GameEntity entity)
        {
            if (entity == null) //if outside, kill him
                kill();
        }

        public GameEntity getSource()
        {
            return source;
        }
    }
}
