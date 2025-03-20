using System;
using System.Data;

namespace 이윤형_콘솔_프로젝트_제작
{
    class Program
    {
        struct Position
        {
            public int x;
            public int y;
        }
        static void Main(string[] args)
        {
            bool gameOver = false;
            char[,] map;
            Position playerPos;
            Position goalPos;
            

            Start(out playerPos, out map, out goalPos);
            while (gameOver == false)
            {
                Render(map, playerPos, goalPos);
                ConsoleKey Key = Input();
                Update(Key, ref playerPos, goalPos, map, ref gameOver);
                
            }
            End(map, playerPos);
        }
        //게임 시작
        static void Start(out Position playerPos, out char[,] map, out Position goalPos)
        {
            

            //플레이어 설정
            playerPos.x = 1;
            playerPos.y = 1;
            //맵 설정
            map = new char[15, 15]
            {
                {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
                {'#', ' ', '#', ' ', '#', ' ', '^', '^', '^', '^', '#', ' ',  ' ',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ',  ' ',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', ' ', '#', '#', '#', ' ', '#', ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', ' ', '#', '#', '#', ' ', '#', ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', ' ', '#', ' ', ' ', ' ', '#', ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', ' ', '#', ' ', ' ', ' ', '#', ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', '#', ' ', '#', '#', '#', ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', ' ', ' ', '#', ' ', '#', '#', '#', ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', '^', '#', ' ',  ' ',' ','#',  ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', '^', '#', ' ',  ' ',' ','#',  ' ', '#',  ' ', '#'},
                {'#', ' ', '#', ' ', '#', ' ', '#', ' ', '#', '#', '#', ' ', '#',  ' ', '#'},
                {'#', ' ', '^', ' ', '#', ' ', '#', ' ',  ' ', ' ',  ' ', ' ', '#',' ', '#'},
                {'#', ' ', ' ', ' ', '#', ' ', '#', ' ',  ' ', ' ',  '^', ' ', '#',  ' ', '#'},
                {'#', '#', '#', '#', '#', '#', '#', '#', '#',  '#', '#', '#', '#', '#', '#'}

            };

            //목적지 설정
            goalPos.x = 13;
            goalPos.y = 13;

            Title();
        }

        static void Title()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("이윤형의 미로찾기에 오신걸 환영합니다.");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine("아무키나 눌러 시작하세요.");

            Console.ReadKey(true);
            Console.Clear();
        }

        static void Render(char[,] map, Position playerPos, Position goalPos)
        {
            Console.SetCursorPosition(0, 0);
            MapPrint(map);
            PlayerPrint(playerPos);
            GoalPrint(goalPos);

        }

        static void MapPrint(char[,] map)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.Write(map[y, x]);

                }
                Console.WriteLine();
            }
        }

        static void PlayerPrint(Position playerPos)
        {
            Console.SetCursorPosition(playerPos.x, playerPos.y);
            Console.Write('P');

        }

        static void GoalPrint(Position goalPos)
        {
            Console.SetCursorPosition(goalPos.x, goalPos.y);
            Console.WriteLine('G');
        }

        static ConsoleKey Input()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            return input;
        }



        static void Update(ConsoleKey Key, ref Position playerPos, Position goalPos, char[,] map, ref bool gameOver)
        {
            Move(Key, ref playerPos, map);
            bool isClear = GameClear(playerPos, goalPos);
            if (isClear || map[playerPos.y, playerPos.x] == '^')
            {
                gameOver = true;
            }

        }
        static void Move(ConsoleKey Key, ref Position playerPos, char[,] map)
        {
            switch (Key)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (map[playerPos.y, playerPos.x - 1] == ' ')
                    {
                        playerPos.x--;
                    }
                    else if (map[playerPos.y, playerPos.x - 1] == '^')
                    {
                        Console.Clear();
                        Console.WriteLine("DIE");
                        playerPos.x--;
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (map[playerPos.y, playerPos.x + 1] == ' ')
                    {
                        playerPos.x++;
                    }
                    else if (map[playerPos.y, playerPos.x + 1] == '^')
                    {
                        Console.Clear();
                        Console.WriteLine("DIE");
                        playerPos.x++;
                    }
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (map[playerPos.y - 1, playerPos.x] == ' ')
                    {
                        playerPos.y--;
                    }
                    else if (map[playerPos.y - 1, playerPos.x] == '^')
                    {
                        Console.Clear();
                        Console.WriteLine("DIE");
                        playerPos.y--;
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (map[playerPos.y + 1, playerPos.x] == ' ')
                    {
                        playerPos.y++;
                    }
                    else if (map[playerPos.y + 1, playerPos.x] == '^')
                    {
                        Console.Clear();
                        Console.WriteLine("DIE");
                        playerPos.y++;
                    }
                    break;
            }

            if (map[playerPos.y, playerPos.x] == '^')
            {
                Console.Clear();
                Console.WriteLine("DIE");
            }
        }  
             
                          
        
                
        
        static bool GameClear(Position playerPos, Position goalPos)
        {
            bool success = (playerPos.x == goalPos.x) && (playerPos.y == goalPos.y);
            return success;
        }

        static void Trap(char[,] map, Position playerPos)
        {
            if (map[playerPos.y, playerPos.x] == '^')
            {
                Console.Clear();
                Console.WriteLine("YOU DIE");
            }
        }

        static void End(char[,] map, Position playerPos)
        {
            if (map[playerPos.y, playerPos.x] == '^')
            {
                Console.Clear();
                Console.WriteLine("YOU DIE");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Game Clear!");
            }

        }


    }
}
