using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KekMan
{
    class GameFieldManager
    {
        char [,] gameField;
        public readonly static char wallSymbol = '8';
        public readonly static char strongWallSymbol = 'H';
        public readonly static char playerSymbol = '@';
        public readonly static char enemySymbol = '#';
        public readonly static char emptySymbol = ' ';
        public readonly static char finishSymbol = '$';
        public static int moveDelay = 200;
        public readonly static int renderDelay = 100;
        Player player;
        List<Enemy> enemyesList;

        string Draw()
        {
            string res="";
            for (int i = 0; i < gameField.GetLength(1); i++)
            {
                for (int j = 0; j < gameField.GetLength(0); j++)
                {
                    res += gameField[j, i];
                }
                res += "\n";
            }
            return res;
        }
        void Render()
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Draw());
        }
        public bool StartGame()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            player.Activate();
            Thread renderThread= new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                AppDomain.CurrentDomain.ProcessExit +=
                delegate
                {
                    Thread.CurrentThread.Abort();
                };
                while (player.IsAlive() && !player.IsFinished())
                {
                    Render();
                    Thread.Sleep(renderDelay);
                }
            });
            renderThread.Start();
            while (player.IsAlive() && !player.IsFinished())
            {
                foreach (Enemy en in enemyesList)
                {
                    bool catchPlayer = en.MoveToPlayer(player.GetX(), player.GetY());
                    if (catchPlayer)
                    {
                        player.Kill();
                        break;
                    }
                }
                Thread.Sleep(moveDelay);
            }
            renderThread.Abort();
            bool res = player.IsFinished();
            return res;
        }
        public void Dispose()
        {
            gameField = null;
            player = null;
            enemyesList = null;
        }
        public GameFieldManager(string levelMap)
        {
            string[] arr = levelMap.Trim().Split('\n');
            gameField = new char[arr[0].Length,arr.Length ];
            enemyesList = new List<Enemy>();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i].Trim();
                for (int j = 0; j < arr[0].Length; j++)
                {
                    gameField[j,i] = arr[i][j];
                }
            }
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    if (gameField[i,j] == playerSymbol && player == null)
                    {
                        player = new Player(gameField, i, j);
                    }
                    else if (gameField[i,j] == enemySymbol)
                    {
                        enemyesList.Add(new Enemy(gameField, i, j));
                    }
                }
            }
            if (player == null)
                throw new Exception("Player not found!");
            
        }
    }
}
