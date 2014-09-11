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

        private float ViewPortWidth;
        private float ViewPortHeight;

        public World()
        {
            // Initialize physics 
            Physics.init(this);

            // Add world to the entity class
            GameEntity.world = this;

            this.field = new GameField((int)width, (int)height);
            this.bulletPool = new BulletPool();
            this.effects = new FXManager();

            this.entities = new List<GameEntity>();
            this.deadEntities = new List<GameEntity>();
            this.spawnEntities = new List<GameEntity>();

            player = new Player(80, 100);
            addEntity(player);

            spawnAsteroid(AsteroidEntity.Type.BIG, new Vector2(100, 100));
        }

        public void spawnAsteroid(AsteroidEntity.Type type, Vector2 pos)
        {
            if (type == AsteroidEntity.Type.BIG)
            {
                AsteroidEntity a = new AsteroidEntity(Assets.getTexture("Graphics/astroid"), type, pos.X, pos.Y);
                addEntity(a);
            }
            else if (type == AsteroidEntity.Type.SMALL)
            {
                for (int i = 0; i < 3; i++)
                {
                    AsteroidEntity a = new AsteroidEntity(Assets.getTexture("Graphics/astroid"), type, pos.X, pos.Y);
                    addEntity(a);
                }
            }
        }

        public void spawnAsteroid()
        {
            spawnAsteroid(AsteroidEntity.Type.BIG, new Vector2(MathUtils.random(field.getBounds().X, field.getBounds().X + width),
                MathUtils.random(field.getBounds().Y, field.getBounds().Y + height)));
        }

        public void update(float delta)
        {
            if (MathUtils.random(500) > 490)
                spawnAsteroid();

            //Update particles
            effects.update(delta);

           // effects.explosion(Vector2.Zero);

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

        private void updateRemoveDeadEntities()
        {
            for (int i = 0; i < deadEntities.Count; i++)
            {
                if (deadEntities[i].GetType() ==  typeof(Bullet)) {
                    bulletPool.ReleaseObject((Bullet)deadEntities[i]);
                }
                entities.Remove(deadEntities[i]);
            }
            deadEntities.Clear();
        }

        private void updateSpawnEntities()
        {
            for (int i = 0; i < spawnEntities.Count; i++)
            {
                entities.Add(spawnEntities[i]);
            }
            spawnEntities.Clear();
        }

        // Returns true if already is inside the field
        public bool putInsideField(GameEntity entity)
        {
            Asteroid.Entity.GameField.FieldHit fd = field.checkInside(entity);
            switch (fd)
            {
                case GameField.FieldHit.Left:
                    entity.setX(field.getBounds().X + entity.getBounds().Width / 2);
                    entity.flipVelocityX();
                    entity.collide(null);
                    break;
                case GameField.FieldHit.Right:
                    entity.setX(field.getBounds().X + field.getBounds().Width - entity.getBounds().Width / 2);
                    entity.flipVelocityX();
                    entity.collide(null);
                    break;
                case GameField.FieldHit.Top:
                    entity.setY(field.getBounds().Y + entity.getBounds().Height / 2);
                    entity.flipVelocityY();
                    entity.collide(null);
                    break;
                case GameField.FieldHit.Bottom:
                    entity.setY(field.getBounds().Y + field.getBounds().Height - entity.getBounds().Height / 2);
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

        public void shoot(GameEntity source, Vector2 pos, Vector2 velocity)
        {
            Bullet b = bulletPool.GetObject();
            b.shoot(source, pos, velocity);
            entities.Add(b);
        }

        public void addEntity(GameEntity e) {
            spawnEntities.Add(e);
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
