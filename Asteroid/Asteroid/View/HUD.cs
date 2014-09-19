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

        // DRAW UI
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

                    if (screen.isHighscore())
                    {
                        // highscore!
                        batch.DrawString(Assets.font, "HIGHSCORE!!",
                         new Vector2(
                              screen.getGraphics().Viewport.Width / 2 - Assets.font.MeasureString("HIGHSCORE!!").Length() / 1, 120), Color.Red, -(float)Math.PI/15, Vector2.Zero, new Vector2(2,2), SpriteEffects.None, 0);
                    }


                    // Score
                    batch.DrawString(Assets.font, "You scored: " + world.getScore(),
                     new Vector2(
                          screen.getGraphics().Viewport.Width / 2 - Assets.font.MeasureString("You scored: " + world.getScore()).Length() / 2, 210), Color.White);

                    // Draw count down
                    if (screen.getStateTime() < 3)
                    {
                        batch.DrawString(Assets.font, ((int)(4 - screen.getStateTime())).ToString(),
                          new Vector2(
                               screen.getGraphics().Viewport.Width / 2 - Assets.font.MeasureString(((int)(4 - screen.getStateTime())).ToString()).Length() / 1,
                               screen.getGraphics().Viewport.Height / 2 + 16), Color.White, 0, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0);
                    }
                    else
                    {
                        // Gameover
                        batch.DrawString(Assets.font, gameOverText,
                          new Vector2(
                               screen.getGraphics().Viewport.Width / 2 - Assets.font.MeasureString(gameOverText).Length() / 2,
                               screen.getGraphics().Viewport.Height / 2 + 16), Color.White);
                    }
                    break;
            }

        }

        // Draw game HUD
        private void drawHUD(SpriteBatch batch)
        {
            batch.DrawString(Assets.font, "Highscore: "  + screen.getHighscore(), new Vector2(10, 10), Color.White);

            batch.DrawString(Assets.font, "Score: " + world.getScore(),
                     new Vector2(
                          screen.getGraphics().Viewport.Width / 2 - Assets.font.MeasureString("Score: " + world.getScore()).Length() / 2, 30), Color.White);


            // draw lives
            for (int i = 0; i < world.getPlayer().getHealth(); i++)
            {
                batch.Draw(Assets.getTexture("Graphics/heart"), new Vector2(i * (32) + 10, 40), Color.White);
            }
        }
    }
}
