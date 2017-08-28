using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyIntoNyx
{
    class Map
    {
        int mapSize;
        int[,] mapData;

        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        private int width, height;
        public int Width
        {
            get { return width; }
        }
        public int Heigth
        {
            get { return height; }
        }

        public Map() { }

        public bool CanJump(Rectangle playerRect)
        {
            int playerx = (playerRect.Left + (playerRect.Width / 2)) / mapSize;
            int playerY = playerRect.Top / mapSize;
            int above = playerY - 1;
            if (above <= 0)
                return false;

            int upTile = mapData[above, playerx];
            return upTile == 0;
        }

        public void Generate(int[,] map, int size)
        {
            mapData = map;
            mapSize = size;
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];
                    if (number > 0)
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
        }
        //int x = 5 > 1 ? 0:1;
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
            {
                tile.Draw(spritebatch);

            }
        }
    }
}
