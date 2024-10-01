using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Numerics;
using Sprint4BeanTeam;
using Sprint4BeanTeam.GameObject.Enemies;

namespace Sprint4BeanTeam
{
    public class CollisionDistributor
    {
        public Grid grid;
        List<Player> players;
        List<IEnemy> enemies;
        List<IItem> items;

        public CollisionDistributor(int cellSize, int gridWidth, int gridHeight)
        {
            this.grid = new Grid(cellSize, gridWidth, gridHeight);
            players = new List<Player>();
            enemies = new List<IEnemy>();
            items = new List<IItem>();
            
        }

        public void ClearObjects(List<Player> players, List<IBlock> blocks, List<IEnemy> enemies, List<IItem> items)
        {
            foreach (Player player in players)
            {
                grid.RemoveObject(player);
            }
            foreach (IBlock block in blocks)
            {
                grid.RemoveObject(block);
            }
            foreach (IEnemy enemy in enemies)
            {
                grid.RemoveObject(enemy);
            }
            foreach (IItem item in items)
            {
                grid.RemoveObject(item);
            }
        }

        public void DistributeObjects(List<Player> players, List<IBlock> blocks, List<IEnemy> enemies, List<IItem> items)
        {
            foreach (Player player in players)
            {
                grid.AddObject(player);
            }
            foreach (IBlock block in blocks)
            {
                grid.AddObject(block);
            }
            foreach (IEnemy enemy in enemies)
            {
                grid.AddObject(enemy);
            }
            foreach (IItem item in items)
            {
                grid.AddObject(item);
            }
            this.enemies = enemies;
            this.players = players;
            this.items = items;
        }

        public void Update()
        {
            playerCollision(players);
            enemyCollision(enemies);
            itemCollision(items);
            grid.Update();
        }

        public void playerCollision(List<Player> players)
        {
            foreach (Player player in players)
            {
                HashSet<IGameObject> collidibles = player.getNearbyObjects();
                PlayerCollisionManager manager = new PlayerCollisionManager(player);
                foreach (IGameObject obj in collidibles)
                {
                    if (obj is Block block)
                        manager.StructuralCollision(block);
                    else if (obj is Enemy enemy)
                    {
                        manager.EnemyCollision((Enemy)enemy);
                    }
                    else if (obj is Item item)
                    {
                        manager.ItemCollision(item);
                    }
                }
            }
        }

        public void enemyCollision(List<IEnemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                HashSet<IGameObject> collidibles = enemy.getNearbyObjects();
                EnemyCollisionManager manager = new EnemyCollisionManager(enemy);
                foreach (IGameObject obj in collidibles)
                {
                    if (obj is Block block)
                        manager.StructuralCollision(block);
                    else if (obj is IPlayer player)
                    {
                        manager.PlayerCollision(player);
                    }
                    else if (obj is Enemy otherEnemy)
                    {
                        manager.EnemyCollision(otherEnemy);
                    }
                }
            }
        }

        public void itemCollision(List<IItem> items)
        {
            foreach (Item item in items)
            {
                HashSet<IGameObject> collidibles = item.getNearbyObjects();
                ItemCollisionManager manager = new ItemCollisionManager(item);
                foreach (IGameObject obj in collidibles)
                {
                    if (obj is Block block)
                        manager.StructuralCollision(block);
                }
            }
        }
    }
}