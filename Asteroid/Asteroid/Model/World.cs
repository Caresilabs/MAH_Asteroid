using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;
using Asteroid.Physic;

namespace Asteroid.Model
{
    public class World
    {
        // World size
        public const float width = 500;
        public const float height = 500;

        private List<GameEntity> entities;
        private Player player;
        private GameField field;

        private float ViewPortWidth;
        private float ViewPortHeight;

        public World()
        {
            // Initialize physics
            Physics.init(this);

            field = new GameField((int)width, (int)height);

            entities = new List<GameEntity>();

            player = new Player(80, 100);
            entities.Add(player);


            Player p = new Player(10, 100);
            entities.Add(p);

            
        }

        public void update(float delta)
        {
            foreach (GameEntity entity in entities)
            {
                entity.update(delta);
                Asteroid.Entity.GameField.FieldHit fd = field.checkInside(entity);
                switch (fd)
                {
                    case GameField.FieldHit.Left:
                        entity.flipVelocityX();
                        entity.setX(field.getBounds().Left);
                        break;
                    case GameField.FieldHit.Right:
                        entity.flipVelocityX();
                        entity.setX(field.getBounds().Right - entity.getBounds().Width);
                        break;
                    case GameField.FieldHit.Top:
                        entity.flipVelocityY();
                        entity.setX(field.getBounds().Top);
                        break;
                    case GameField.FieldHit.Bottom:
                        entity.flipVelocityY();
                        entity.setX(field.getBounds().Bottom - entity.getBounds().Height);
                        break;
                    case GameField.FieldHit.Inside:
                        break;
                    default:
                        break;
                }
            }
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

    }
}
