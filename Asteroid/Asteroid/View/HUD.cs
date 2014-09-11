using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Controller;
using Asteroid.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid.View
{
    public class HUD
    {
        private World world;
        private GameScreen screen;

        private String pausedText = "Press the 'anykey' to start the game!";
        private String gameOverText = "Press the 'anykey' to restart the game!";

        public HUD(GameScreen game)
        {
            this.world = game.getWorld();
            this.screen = game;
        }

        public void draw(SpriteBatch batch)
        {
            switch (screen.getState())
            {
                case Asteroid.Controller.GameScreen.GameState.PAUSED:
                    batch.DrawString(Assets.font, pausedText,
                       new Vector2(
                            screen.getGraphics().Viewport.Width / 2 - Assets.font.MeasureString(pausedText).Length() / 2,
                            screen.getGraphics().Viewport.Height / 2 - 32), Color.White);
                    break;
                case Asteroid.Controller.GameScreen.GameState.RUNNING:
                    drawHUD(batch);
                    break;
                case Asteroid.Controller.GameScreen.GameState.GAMEOVER:
                    batch.DrawString(Assets.font, gameOverText,
                      new Vector2(
                           screen.getGraphics().Viewport.Width / 2 - Assets.font.MeasureString(gameOverText).Length() / 2,
                           screen.getGraphics().Viewport.Height / 2 - 32), Color.White);
                    break;
            }
            
        }

        private void drawHUD(SpriteBatch batch)
        {
            //todo
             batch.DrawString(Assets.font, world.getPlayer().getPosition() + "", new Vector2(10, 10), Color.White);
        }
    }
}
