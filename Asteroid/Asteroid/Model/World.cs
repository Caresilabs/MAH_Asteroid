using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Physic;
using Microsoft.Xna.Framework;
using Asteroid.Tools;

namespace Asteroid.Model
{
    public class World
    {
        // World size
        public const float width = 1000;
        public const float height = 1000;

        private List<GameEntity> entities;
        private List<GameEntity> deadEntities;
        private List<GameEntity> spawnEntities;

        private Player player;
        private GameField field;
        private BulletPool bulletPool;
        private FXManager effects;

        private float score;
        private float spawnTime;
        private float gameTime;

        public World()
        {
            // Initialize physics 
            Physics.init(this);

            // Add world to the entity class
            GameEntity.world = this;

            this.score = 0;
            this.gameTime = 0;
            this.spawnTime = 100;
            this.field = new GameField((int)width, (int)height);
            this.bulletPool = new BulletPool();
            this.effects = new FXManager();

            // Set up entity lists
            this.entities = new List<GameEntity>();
            this.deadEntities = new List<GameEntity>();
            this.spawnEntities = new List<GameEntity>();

            this.player = new Player(80, 100);
            addEntity(player);
        }

        // Spawn an asteroid, small or big
        public void spawnAsteroid(AsteroidEntity.Type type, Vector2 pos)
        {
            if (type == AsteroidEntity.Type.BIG)
            {
                AsteroidEntity a = new AsteroidEntity(Assets.getTexture("Graphics/astroid" + MathUtils.random(3)), type, pos.X, pos.Y);
                addEntity(a);
            }
            else if (type == AsteroidEntity.Type.SMALL)
            {
                for (int i = 0; i < 3; i++)
                {
                    AsteroidEntity a = new AsteroidEntity(Assets.getTexture("Graphics/astroid" + MathUtils.random(3)), type,
                        pos.X + -AsteroidEntity.widthSmall/2 + (i * AsteroidEntity.widthSmall/2),
                        pos.Y + -AsteroidEntity.heightSmall/2 + (i * AsteroidEntity.heightSmall/2));//MathUtils.random(-AsteroidEntity.heightSmall, AsteroidEntity.heightSmall));
                    addEntity(a);
                }
            }
        }

        // Spawn a big asteroid at random location
        public void spawnAsteroid(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Vector2 ranPos;
                for (int p = 0; p < 100; p++)
                {
                    ranPos = new Vector2(MathUtils.random(field.getBounds().X, field.getBounds().X + field.getBounds().Width),
                       MathUtils.random(field.getBounds().Y, field.getBounds().Y + field.getBounds().Height));

                    // Check if not overlapping with another entity
                    if (! overlapWithAsteroid(ranPos))
                    {
                        spawnAsteroid(AsteroidEntity.Type.BIG, ranPos);
                        break;
                    }
                }
            }
        }

        public void update(float delta)
        {
            //Update particles
            effects.update(delta);

            if (player.isEntityAlive())
            { 
                // Add survial time score if not dead
                addScore(delta * 2);

                gameTime += delta;

                // Handle asteroid spawn
                if (spawnTime > 3.5f)
                {
                    spawnAsteroid((int)Math.Min(5, 2 + (gameTime / 20))); // 2 at spawn and then increase with time
                    spawnTime = 0;
                }
                else
                {
                    spawnTime += delta;
                }
            }

            // update entities
            foreach (GameEntity entity in entities)
            {
                entity.update(delta);
                // Check twice he might be wrong both y and x, not a good way to do it but LOW priority atm
                if (!putInsideField(entity))
                {
                    putInsideField(entity);
                }

                // remove him if he is dead
                if (!entity.isEntityAlive())
                {
                    deadEntities.Add(entity);
                }
            }
            updateRemoveDeadEntities();
            updateSpawnEntities();
        }

        // Remove dead entities from game
        private void updateRemoveDeadEntities()
        {
            for (int i = 0; i < deadEntities.Count; i++)
            {
                if (deadEntities[i].GetType() == typeof(Bullet))
                {
                    bulletPool.ReleaseObject((Bullet)deadEntities[i]);
                }
                entities.Remove(deadEntities[i]);
            }
            deadEntities.Clear();
        }

        // Spawn new entities into game
        private void updateSpawnEntities()
        {
            for (int i = 0; i < spawnEntities.Count; i++)
            {
                entities.Add(spawnEntities[i]);
            }
            spawnEntities.Clear();
        }

        // Returns true if already is inside the game field
        public bool putInsideField(GameEntity entity)
        {
            Asteroid.Entity.GameField.FieldHit fd = field.checkInside(entity);
            switch (fd)
            {
                case GameField.FieldHit.Left:
                    entity.setX(field.getBounds().X + GameField.thickness);
                    entity.flipVelocityX();
                    entity.collide(null);
                    break;
                case GameField.FieldHit.Right:
                    entity.setX(field.getBounds().X + field.getBounds().Width - entity.getBounds().Width);
                    entity.flipVelocityX();
                    entity.collide(null);
                    break;
                case GameField.FieldHit.Top:
                    entity.setY(field.getBounds().Y + GameField.thickness);
                    entity.flipVelocityY();
                    entity.collide(null);
                    break;
                case GameField.FieldHit.Bottom:
                    entity.setY(field.getBounds().Y + field.getBounds().Height - entity.getBounds().Height);
                    entity.flipVelocityY();
                    entity.collide(null);
                    break;
                case GameField.FieldHit.Inside:
                    return true;
                default:
                    break;
            }
            return false;
        }

        // Check if an asteroid is going to overlap with an existing asteroid
        public bool overlapWithAsteroid(Vector2 pos)
        {
            Rectangle tempRect = new Rectangle((int)pos.X, (int)pos.Y, (int)AsteroidEntity.widthBig, (int)AsteroidEntity.heightBig);

            // Check active entities
            foreach (var item in entities)
            {
                if (item.GetType() == typeof(AsteroidEntity) || item.GetType() == typeof(Player))
                {
                    // Check if new spawn is a good place and also not on the player
                    if (tempRect.Intersects(item.getBounds()) || tempRect.Intersects(getPlayer().getSafeZoneBounds()))
                    {
                        return true;
                    }
                }
            }

            // Check spawn list
            foreach (var item in spawnEntities)
            {
                if (item.GetType() == typeof(AsteroidEntity) || item.GetType() == typeof(Player))
                {
                    // Check if new spawn is a good place and also not on the player
                    if (tempRect.Intersects(item.getBounds()) || tempRect.Intersects(getPlayer().getSafeZoneBounds()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Shoot from @x, @y with @velocity
        public void shoot(GameEntity source, float x, float y, Vector2 velocity)
        {
            Bullet b = bulletPool.GetObject();
            b.shoot(source, x, y, velocity);
            entities.Add(b);
        }

        public void addEntity(GameEntity e)
        {
            spawnEntities.Add(e);
        }

        public void addScore(float score)
        {
            this.score += score;
        }

        public int getScore()
        {
            return (int)score;
        }

        public List<GameEntity> getEntities()
        {
            return entities;
        }

        public GameField getField()
        {
            return field;
        }

        public Player getPlayer()
        {
            return player;
        }

        public FXManager getEffects()
        {
            return effects;
        }
    }
}
