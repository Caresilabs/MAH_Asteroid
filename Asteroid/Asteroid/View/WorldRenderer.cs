using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Model;
using Microsoft.Xna.Framework.Graphics;
using Asteroid.Tools;
using Microsoft.Xna.Framework;

namespace Asteroid.View
{
    public class WorldRenderer
    {
        private World world;
        private Camera2D camera;

        private Texture2D bg;
        private Rectangle bgBounds;

        public WorldRenderer(World world)
        {
            this.world = world;
            this.camera = new Camera2D();
            this.camera.setZoom(1.7f);

            this.bg = Assets.getTexture("Graphics/stars");
            this.bgBounds = new Rectangle(world.getField().getBounds().X - 400, world.getField().getBounds().Y - 400,
                world.getField().getBounds().Width*2, world.getField().getBounds().Height*2);
        }

        public void render(SpriteBatch batch, GraphicsDevice device)
        {
            // begin batch with cameras matrix
            batch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        SamplerState.PointClamp,
                        null,
                        null,
                        null,
                        camera.getMatrix(device));

            drawBackground(batch);

            world.getField().draw(batch);

            drawEntities(batch);

            world.getEffects().draw(batch);

            batch.End();
        }

        private void drawEntities(SpriteBatch batch)
        {
            // todo render game
            foreach (GameEntity entity in world.getEntities())
            {
                entity.draw(batch);
            }
        }

        private void drawBackground(SpriteBatch batch)
        {
            batch.Draw(bg, bgBounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public Camera2D getCamera()
        {
            return camera;
        }
    }
}
