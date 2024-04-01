using System;

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

            ConsoleKeyInfo pressKey;
            
            int countStar = 0;
            int pacmanPositionX = 1;
            int pacmanPositionY = 1;
            int score = 0;
            int scorePosition = 32;

            bool isCollectedAllStars = false;
            
            countStar = DrawMap(map, countStar);
            
            while (isCollectedAllStars == false)
            {
                Console.Clear();
 
                ShowMap(map);

                DrawPacman(pacmanPositionX, pacmanPositionY);

                DrawScore(scorePosition, score);

                score = KeepScore(map, pacmanPositionX, pacmanPositionY, score);
                
                pressKey = Console.ReadKey();
                
                HandleInput(pressKey, ref pacmanPositionX, ref pacmanPositionY, map);

                isCollectedAllStars = CollectAllStars(score, countStar);
            }
        }

        static int DrawMap(char[,] map, int count)
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

                        if (map[x, y] == star)
                        {
                           count++; 
                        }
                    }
                }

                return count;
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

        static void HandleInput(ConsoleKeyInfo pressKey, ref int pacmanPositionX, ref int pacmanPositionY, char[,] map)
        {
            int[] direction = GetDirection(pressKey);

            MovePlayer(map, ref pacmanPositionX, ref pacmanPositionY, direction);
        }

        static void MovePlayer(char[,] map, ref int pacmanPositionX, ref int pacmanPositionY, int[] direction)
        {
            char wall = '#';

            int nextPacmanPositionX = pacmanPositionX + direction[0];
            int nextPacmanPositionY = pacmanPositionY + direction[1];

            char nextCell = map[nextPacmanPositionY, nextPacmanPositionX];

            if (nextCell != wall)
            {
                pacmanPositionX = nextPacmanPositionX;
                pacmanPositionY = nextPacmanPositionY;
            }
        }

        static int[] GetDirection(ConsoleKeyInfo pressKey)
        {
            const ConsoleKey CommandUp = ConsoleKey.W;
            const ConsoleKey CommandDown = ConsoleKey.S;
            const ConsoleKey CommandLeft = ConsoleKey.A;
            const ConsoleKey CommandRight = ConsoleKey.D;
            
            int[] direction = { 0, 0 };

            if (pressKey.Key == CommandUp)
                direction[1] = -1;
            else if (pressKey.Key == CommandDown)
                direction[1] = 1;
            else if (pressKey.Key == CommandLeft)
                direction[0] = -1;
            else if (pressKey.Key == CommandRight)
                direction[0] = 1;

            return direction;
        }

        static void DrawPacman(int pacmanPositionX, int pacmanPositionY)
        {
            char pacman = '@';
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(pacmanPositionX, pacmanPositionY);
            Console.WriteLine(pacman);
        }

        static int KeepScore(char[,] map, int pacmanPositionX, int pacmanPositionY, int score)
        {
            char empty = ' ';
            char star = '*';

            if (map[pacmanPositionY, pacmanPositionX] == star)
            {
                score++;
                map[pacmanPositionY, pacmanPositionX] = empty;
            }

            return score;
        }

        static void DrawScore(int position, int score)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(position, 0);
            Console.Write($"Score: {score}");
        }

        static bool CollectAllStars(int score, int count)
        {
            return count == score;
        }
    }
}
