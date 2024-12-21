using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Snake
    {
        private LinkedList<Position> body;
        private Direction direction;
        private bool growing;

        public Snake()
        {
            body = new LinkedList<Position>();
            body.AddLast(new Position(10, 10));
            direction = Direction.Right;
            growing = false;
        }

        public void Move()
        {
            Position head = body.First.Value;
            Position newHead = head;

            switch (direction)
            {
                case Direction.Up:
                    newHead = new Position(head.X, head.Y - 1);
                    break;
                case Direction.Down:
                    newHead = new Position(head.X, head.Y + 1);
                    break;
                case Direction.Left:
                    newHead = new Position(head.X - 1, head.Y);
                    break;
                case Direction.Right:
                    newHead = new Position(head.X + 1, head.Y);
                    break;
            }

            if (!growing)
            {
                body.RemoveLast();
            }
            else
            {
                growing = false;
            }

            body.AddFirst(newHead);
        }

        public void OnKeyPress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (direction != Direction.Down) direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (direction != Direction.Up) direction = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    if (direction != Direction.Right) direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (direction != Direction.Left) direction = Direction.Right;
                    break;
            }
        }

        public bool HasEaten(Food food)
        {
            return body.First.Value.Equals(food.Position);
        }

        public void Grow()
        {
            growing = true;
        }

        public bool HasCollidedWithWall(int width, int height)
        {
            Position head = body.First.Value;
            return head.X < 0 || head.X >= width || head.Y < 0 || head.Y >= height;
        }

        public bool HasCollidedWithSelf()
        {
            Position head = body.First.Value;
            return body.Skip(1).Any(p => p.Equals(head));
        }

        public void Draw()
        {
            foreach (var position in body)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write('O');
            }
        }
    }
}
