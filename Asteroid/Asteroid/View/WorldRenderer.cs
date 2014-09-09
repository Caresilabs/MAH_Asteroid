using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Model;
using Microsoft.Xna.Framework.Graphics;
using Asteroid.Tools;

namespace Asteroid.View
{
    public class WorldRenderer
    {
        private World world;
        private Camera2D camera;

        public WorldRenderer(World world)
        {
            this.world = world;
            this.camera = new Camera2D();
        }

        public void render(SpriteBatch batch, GraphicsDevice device)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        camera.getMatrix(device));

            world.getField().draw(batch);

            // todo render game
            foreach (GameEntity entity in world.getEntities())
            {
                entity.draw(batch);
            }

            batch.End();
        }

        public Camera2D getCamera()
        {
            return camera;
        }
    }
}
