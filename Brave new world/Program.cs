using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Brave_new_world
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sizeLineMap = 15;
            int sizeColumMap = 30;

            char[,] map = new char[sizeLineMap, sizeColumMap];

            Console.CursorVisible = false;

            ConsoleKeyInfo pressKey = new ConsoleKeyInfo('W', ConsoleKey.W, false, false, false);


            int pacmanX = 1;
            int pacmanY = 1;
            int score = 0;
            int scorePosition = 32;

            DrawMap(map);
            
            while (true)
            {
                Console.Clear();
 
                ShowMap(map);

                DrawPacman(pacmanX, pacmanY);

                DrawScore(scorePosition,score);

                pressKey = Console.ReadKey();

                HandleInput(pressKey, ref pacmanX, ref pacmanY, map, ref score);
            }
        }

        static void DrawMap(char[,] map)
        {
            Random random = new Random();

            char wall = '#';
            char empty = ' ';
            char star = '*';

            int wallLine = map.GetLength(0) - 1;
            int wallColum = map.GetLength(1) - 1;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[0, y] = wall;
                    map[x, 0] = wall;
                    map[wallLine, y] = wall;
                    map[x, wallColum] = wall;

                    if (map[x, y] != wall)
                    {
                        map[x, y] = empty;
                        map[x, y + random.Next(map.GetLength(1) - y)] = star;
                    };
                }
            }
        }

        static void ShowMap(char[,] map)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++) 
                {
                    Console.Write(map[x,y]);
                }

                Console.WriteLine();
            }
        }

        static void HandleInput(ConsoleKeyInfo pressKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDirection(pressKey);

            char empty = ' ';
            char star = '*';

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            char nextCell = map[nextPacmanPositionY, nextPacmanPositionX];

            if (nextCell == empty || nextCell == star)
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;

                if (nextCell == star)
                {
                    score++;
                    map[nextPacmanPositionY, nextPacmanPositionX] = empty;
                }
            }
        }

        static int[] GetDirection(ConsoleKeyInfo pressKey)
        {
            int[] direction = { 0, 0 };

            if (pressKey.Key == ConsoleKey.W)
                direction[1] = -1;
            else if (pressKey.Key == ConsoleKey.S)
                direction[1] = 1;
            else if (pressKey.Key == ConsoleKey.A)
                direction[0] = -1;
            else if (pressKey.Key == ConsoleKey.D)
                direction[0] = 1;

            return direction;
        }

        static void DrawPacman(int pacmanPositionX, int pacmanPositionY)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pacmanPositionX, pacmanPositionY);
            Console.WriteLine('@');
        }

        static void DrawScore(int position, int score)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(position, 0);
            Console.Write($"Score: {score}");
        }
    }
}
