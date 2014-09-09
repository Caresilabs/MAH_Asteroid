using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroid.Physic;
using Asteroid.Tools;

namespace Asteroid.Entity
{
    public abstract class GameEntity : ICollidable
    {
        // Defaults
        private float maxSpeed = 1;
        private float speed = 2;

        private Texture2D texture;

        private Vector2 position;
        private Vector2 velocity;
        private Vector2 acceleration;
        private Vector2 origin;

        private Rectangle bounds;

        private float width;
        private float height;

        private float rotation;

        public GameEntity(Texture2D tex, float x, float y, float width, float height)
        {
            this.texture = tex;
            this.position = new Vector2(x, y);
            this.velocity = new Vector2();
            this.acceleration = new Vector2();
            this.origin = new Vector2();
            this.bounds = new Rectangle();
            this.width = width;
            this.height = height;

            updateBounds();
        }

        public virtual void update(float delta)
        {
            velocity.X += acceleration.X * delta;
            velocity.Y += acceleration.Y * delta;

            // Friction
            velocity.X *= Math.Min(delta * 70, .98f);
            velocity.Y *= Math.Min(delta * 70, .98f);

            MathUtils.clamp(velocity, -maxSpeed, maxSpeed);

            position.X += velocity.X * delta;
            position.Y += velocity.Y * delta;

            updateBounds();
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(texture, bounds, null, Color.White, rotation, origin, SpriteEffects.None, 0);
        }

        public abstract void collide(GameEntity entity);

        public void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;

            updateBounds();
        }

        public void setX(float x)
        {
            position.X = x;
            updateBounds();
        }

        public void setY(float y)
        {
            position.Y = y;
            updateBounds();
        }

        private void updateBounds()
        {
            // Position
            this.bounds.X = (int)position.X;
            this.bounds.Y = (int)position.Y;
            // Width
            this.bounds.Width = (int)width;
            this.bounds.Height = (int)height;

            this.origin.X = texture.Width / 2;
            this.origin.Y = texture.Height / 2;
        }

        public void rotate(float radians)
        {
            rotation += radians;
        }

        public void setRotation(float radians)
        {
            rotation = radians;
        }

        public float getRotation()
        {
            return rotation;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void addAcceleration(float x, float y)
        {
            acceleration.X += x;
            acceleration.Y += y;

            MathUtils.clamp(acceleration, -maxSpeed, maxSpeed);
        }

        public void setAcceleration(float x, float y)
        {
            this.acceleration.X = x;
            this.acceleration.Y = y;
        }

        public void setSpeed(float speed)
        {
            this.speed = speed;
        }

        public float getSpeed()
        {
            return speed;
        }

        public void setMaxSpeed(float maxSpeed)
        {
            this.maxSpeed = maxSpeed;
        }

        public float getMaxSpeed()
        {
            return maxSpeed;
        }

        public void flipVelocity()
        {
            velocity.X *= -1;
            velocity.Y *= -1;
        }

        public void flipVelocityX()
        {
            velocity.X *= -1;
        }

        public void flipVelocityY()
        {
            velocity.Y *= -1;
        }

        public Vector2 getAcceleration()
        {
            return acceleration;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public void setTexture(Texture2D tex)
        {
            this.texture = tex;
        }
    }
}
