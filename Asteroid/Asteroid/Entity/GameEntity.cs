using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid.Entity
{
    public abstract class GameEntity
    {
        private float maxSpeed = 10;


        private Vector2 position;
        private Vector2 velocity;
        private Vector2 acceleration;

        public GameEntity(float x, float y)
        {
            position = new Vector2(x, y);
            velocity = new Vector2();
            acceleration = new Vector2();
        }

        public virtual void update(float delta) {
            velocity.X += acceleration.X * delta;
            velocity.Y += acceleration.Y * delta;


            position.X += velocity.X * delta;
            position.X += velocity.Y * delta;
        }

        public abstract void draw(SpriteBatch batch);

        public void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }
    }
}
