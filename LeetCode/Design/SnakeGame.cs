using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Design
{

    public class SnakeGameTest
    {
        [Test]
        public void Test()
        {
            var g = new SnakeGame(3, 3, new int[,] {{2, 0}, {0, 0}, {0, 2}, {2, 2}});

            var m = g.Move("D");
            m = g.Move("D");
            m = g.Move("R");
            m = g.Move("U");
            m = g.Move("U");
            m = g.Move("L");
            m = g.Move("D");
            m = g.Move("R");
            m = g.Move("R");
        }
    }

    public class SnakeGame
    {

        /** Initialize your data structure here.
            @param width - screen width
            @param height - screen height 
            @param food - A list of food positions
            E.g food = [[1,1], [1,0]] means the first food is positioned at [1,1], the second is at [1,0]. */

        private int len;
        private int rows;
        private int cols;
        public LinkedList<Position> snake;
        private int[,] food;
        public SnakeGame(int width, int height, int[,] food)
        {
            this.rows = height;
            this.cols = width;
            this.food = food;

            snake = new LinkedList<Position>();
            snake.AddFirst(new Position(0, 0));
            len = 0;
        }

        /** Moves the snake.
            @param direction - 'U' = Up, 'L' = Left, 'R' = Right, 'D' = Down 
            @return The game's score after the move. Return -1 if game over. 
            Game over when snake crosses the screen boundary or bites its body. */
        public int Move(string direction)
        {


            // Get head position and move head.
            Position cur = new Position(snake.First.Value.x, snake.First.Value.y);

            switch (direction)
            {
                case "U":
                    cur.x--; break;
                case "L":
                    cur.y--; break;
                case "R":
                    cur.y++; break;
                case "D":
                    cur.x++; break;
            }

            // Check if head is crashed against wall.
            if (cur.x < 0 || cur.x >= rows || cur.y < 0 || cur.y >= cols) return -1;

            // Check if head is crashed against body.
            var index = 0;
            foreach (var pos in snake)
            {
                if (pos.IsEqual(cur) && index != 0)
                {
                    return -1;
                }

                index++;
            }

            // Move head
            snake.AddFirst(cur);

            // Increase body length if head encountered food.
            if (len < food.GetLength(0))
            {
                var p = new Position(food[len, 0], food[len, 1]);
                if (cur.IsEqual(p))
                {
                    len++;
                }
            }

            // Move tail.
            while (snake.Count > len + 1)
            {
                Console.WriteLine(snake.Last.Value.x +" " + snake.Last.Value.y);
                snake.RemoveLast();
            }

            return len;
        }
    }

    public class Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsEqual(Position p)
        {
            return this.x == p.x && this.y == p.y;
        }
    }

}
