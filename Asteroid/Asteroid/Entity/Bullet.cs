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
        public static int bulletSpeed = 1000;

        private static int bulletSize = 8;
        private GameEntity source;

        public Bullet()
            : base(Assets.getTexture("Graphics/pixel"), 0, 0, bulletSize, bulletSize)
        {
            zLayer = .9f;
        }

        public override void update(float delta)
        {
            base.update(delta);
            Physics.processCollision(this);
        }

        public void shoot(GameEntity source, float x, float y, Vector2 dir)
        {
            this.source = source;
            revivie();
            setMaxSpeed(bulletSpeed);
            setPosition(x, y);
            setVelocity(dir.X, dir.Y);
            setFriction(0);
        }

        public void reset()
        {
            source = null;
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
