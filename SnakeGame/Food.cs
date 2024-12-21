using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Food
    {
        public Position Position { get; private set; }

        public Food(int width, int height, Random random)
        {
            Position = new Position(random.Next(width), random.Next(height));
        }

        public void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write('*');
        }
    }
}
