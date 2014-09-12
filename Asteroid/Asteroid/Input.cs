using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Microsoft.Xna.Framework.Input;
using Asteroid.Tools;
using Asteroid.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Asteroid.Controller;

namespace Asteroid
{
    public class Input
    {
        private GameEntity entity;
        private World world;
        private GameScreen screen;

        private float shootTime;
        private float reloadTime = .25f;

        public Input(GameScreen game, GameEntity controlEntity)
        {
            this.entity = controlEntity;
            this.world = game.getWorld();
            this.screen = game;
        }

        public void update(float delta)
        {
            // Vertical
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //float y = (float)Math.Sin(-entity.getRotation() + Math.PI / 2);
               // float x = (float)Math.Cos(-entity.getRotation() + Math.PI / 2);

                entity.addAcceleration(0,  -entity.getSpeed());
            } else 
            
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                entity.addAcceleration(0, entity.getSpeed());
            }
            else
            {
                entity.setAcceleration(entity.getAcceleration().X, 0);
            }


            // Horizontal
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                entity.addAcceleration(-entity.getSpeed(), 0);
                //entity.rotate(-delta*3);
            } else 
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                entity.addAcceleration(entity.getSpeed(), 0);
               // entity.rotate(delta*3);
            }
            else
            {
                entity.setAcceleration(0, entity.getAcceleration().Y);
            }

            if (!Keyboard.GetState().IsKeyDown(Keys.W) && 
                !Keyboard.GetState().IsKeyDown(Keys.S) &&
                !Keyboard.GetState().IsKeyDown(Keys.A) &&
                !Keyboard.GetState().IsKeyDown(Keys.D))
            {
               // entity.setAcceleration(0, 0);
            }

            // Calc angle
            Vector2 direction = new Vector2(Mouse.GetState().X - screen.getGraphics().Viewport.Width/2,
                Mouse.GetState().Y - screen.getGraphics().Viewport.Height/2);
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            entity.setRotation(rotation + (float)Math.PI/2);


            // Handle Shooting
            shootTime -= delta;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (shootTime < 0)
                {
                    float y = -(float)Math.Sin(-entity.getRotation() + Math.PI / 2);
                    float x = (float)Math.Cos(-entity.getRotation() + Math.PI / 2);

                    world.shoot(entity, entity.getPosition().X + entity.getBounds().Width/2,
                        entity.getPosition().Y + entity.getBounds().Height / 2,
                        new Vector2(x * Bullet.bulletSpeed, y * Bullet.bulletSpeed));
                    shootTime = reloadTime;
                }
            }
        }
    }
}
