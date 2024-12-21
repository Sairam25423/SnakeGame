using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SnakeGame
{
    internal class Game
    {
        private const int Width = 40;
        private const int Height = 20;
        private bool gameRunning;
        private int score;
        private Snake snake;
        private Food food;
        private Random random;

        public Game()
        {
            Console.WindowWidth = Width;
            Console.WindowHeight = Height;
            Console.CursorVisible = false;

            random = new Random();
            snake = new Snake();
            food = new Food(Width, Height, random);
            score = 0;
        }

        public void Run()
        {
            gameRunning = true;
            Task.Run(() => ReadInput());
            while (gameRunning)
            {
                Update();
                Draw();
                Thread.Sleep(100);
            }
        }

        private void ReadInput()
        {
            while (gameRunning)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    snake.OnKeyPress(key);
                }
            }
        }

        private void Update()
        {
            snake.Move();
            if (snake.HasCollidedWithWall(Width, Height) || snake.HasCollidedWithSelf())
            {
                gameRunning = false;
            }

            if (snake.HasEaten(food))
            {
                score++;
                snake.Grow();
                food = new Food(Width, Height, random);
            }
        }

        private void Draw()
        {
            Console.Clear();
            snake.Draw();
            food.Draw();
            Console.SetCursorPosition(0, 0);
            Console.Write($"Score: {score}");
        }
    }
}
