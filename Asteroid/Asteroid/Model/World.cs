using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroid.Entity;

namespace Asteroid.Model
{
    public class World
    {
        private List<GameEntity> entities;
        private Player player;

        public World()
        {
            entities = new List<GameEntity>();

            player = new Player(100, 100);


            entities.Add(player);
        }

        public void update(float delta)
        {
            foreach (GameEntity entity in entities)
            {
                entity.update(delta);
            }
        }

        public List<GameEntity> getEntities()
        {
            return entities;
        }

        public Player getPlayer()
        {
            return player;
        }

    }
}
