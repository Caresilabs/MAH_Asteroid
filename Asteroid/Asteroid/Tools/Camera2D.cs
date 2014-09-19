using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroid.Tools
{
    /**
     * Camera makes the live a lot easier by translating a matrix and then tell the spritebatch to translate all objects by the values
     * set in this class
     */
    public class Camera2D
    {
        private float zoom;
        private float rotation;

        private Matrix transform;
        private Vector2 position;

        public Camera2D()
        {
            this.rotation = 0f;
            this.zoom = 1f;
            this.position = Vector2.Zero;
        }

        // Gets the matrix used by the spritebatch
        public Matrix getMatrix(GraphicsDevice graphicsDevice)
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
           
            return transform;
        }

        public float getZoom()
        {
            return zoom;
        }

        public void setZoom(float zoom)
        {
            this.zoom = zoom;
            if (this.zoom < 0.1f) this.zoom = 0.1f;
        }

        public float getRotation()
        {
            return rotation;
        }

        public void setRotation(float rot)
        {
            rotation = rot;
        }

        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void setPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
        }
    }
}
