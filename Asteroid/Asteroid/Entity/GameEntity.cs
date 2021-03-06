﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroid.Physic;
using Asteroid.Tools;
using Asteroid.Model;

namespace Asteroid.Entity
{
    /*
     * Basic game entity
     */
    public abstract class GameEntity : ICollidable
    {
        public static World world;

        public float zLayer = 0;

        private float maxSpeed = 2;
        private float speed = 40;

        private float friction = .1f;

        private bool isAlive;

        private Texture2D texture;

        private Vector2 position;
        private Vector2 velocity;
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
            this.origin = new Vector2();
            this.bounds = new Rectangle();
            this.width = width;
            this.height = height;
            this.isAlive = true;

            updateBounds();
        }

        public virtual void update(float delta)
        {
            // Friction
            velocity.X *= Math.Min((1 - friction), 1f);
            velocity.Y *= Math.Min((1 - friction), 1f);

            MathUtils.clamp(velocity, -maxSpeed, maxSpeed);

            position.X += velocity.X * delta;
            position.Y += velocity.Y * delta;

            updateBounds();
        }

        private  Rectangle drawRect = new Rectangle();
        public virtual void draw(SpriteBatch batch)
        {
            this.drawRect.Width = (int)width;
            this.drawRect.Height = (int)height;
            this.drawRect.X = (int)position.X + (int)width/2;
            this.drawRect.Y = (int)position.Y + (int)height/2;

            batch.Draw(texture, drawRect, null, Color.White, rotation, origin, SpriteEffects.None, zLayer);
        }

        public abstract void collide(GameEntity entity);

        public void kill()
        {
            isAlive = false;
        }

        public void revivie()
        {
            isAlive = true;
        }

        public bool isEntityAlive()
        {
            return isAlive;
        }

        public void setPosition(float x, float y)
        {
            this.position.X = x;
            this.position.Y = y;

            updateBounds();
        }

        public void setX(float x)
        {
            this.position.X = x;
            updateBounds();
        }

        public void setY(float y)
        {
            this.position.Y = y;
            updateBounds();
        }

        public void setSize(float width, float height)
        {
            this.width = width;
            this.height = height;

            updateBounds();
        }

        private void updateBounds()
        {
            // Position
            this.bounds.X = (int)position.X; //- (int)width/2;
            this.bounds.Y = (int)position.Y;// - (int)height/2;
            // Width
            this.bounds.Width = (int)width;
            this.bounds.Height = (int)height;

            this.origin.X = texture.Width / 2;
            this.origin.Y = texture.Height / 2;
        }

        public void rotate(float radians)
        {
            this.rotation += radians;
        }

        public void setRotation(float radians)
        {
            this.rotation = radians;
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

        public void addVelocity(float x, float y)
        {
            velocity.X += x;
            velocity.Y += y;

            MathUtils.clamp(velocity, -maxSpeed, maxSpeed);
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

        public void setVelocity(float x, float y)
        {
            this.velocity.X = x;
            this.velocity.Y = y;
        }

        public void addVelocityStep()
        {
            position.X += velocity.X * 1/30f;
            position.Y += velocity.Y * 1/30f;
        }

        public Vector2 getVelocity()
        {
            return velocity;
        }

        public void flipVelocity()
        {
            this.velocity.X *= -1;
            this.velocity.Y *= -1;
        }

        public void flipVelocityX()
        {
            this.velocity.X *= -1;
        }

        public void flipVelocityY()
        {
            this.velocity.Y *= -1;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public void setTexture(Texture2D tex)
        {
            this.texture = tex;
        }

        public void setFriction(float friction)
        {
            this.friction = friction;
        }
    }
}
