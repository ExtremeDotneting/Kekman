using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KekMan
{
    class Player : Creature
    {
        bool disposed;
        bool finished;
        public Player(char[,] gameField, int x, int y) : base(gameField,x,y)
        {
            currentSymbol = GameFieldManager.playerSymbol;
            finished = false;
            disposed = false;
        }
        protected override bool CanMoveTo(int x, int y)
        {
            return x >= 0 && y >= 0 && x < gameField.GetLength(0) && y < gameField.GetLength(1) && gameField[x, y] == GameFieldManager.emptySymbol; 
        }
        public void Activate()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                AppDomain.CurrentDomain.ProcessExit +=
                delegate
                {
                    Thread.CurrentThread.Abort();
                };
                while (!disposed)
                {
                    char key = Console.ReadKey(true).KeyChar;
                    switch (key)
                    {
                        case 'w':
                            MoveUp();
                            break;
                        case 's':
                            MoveDown();
                            break;
                        case 'a':
                            MoveLeft();
                            break;
                        case 'd':
                            MoveRight();
                            break;
                    }
                    //Thread.Sleep(GameFieldManager.moveDelay);
                }
                Thread.CurrentThread.Abort();
            }).Start();
        }
        void CheckIfFinish(int x, int y)
        {
            if (gameField[x, y] == GameFieldManager.finishSymbol)
                finished = true;
        }
        public override void MoveUp()
        {
            CheckIfFinish(x, y-1);
            TryRelocate(x, y - 1);
        }
        public override void MoveDown()
        {
            CheckIfFinish(x, y+1);
            TryRelocate(x, y + 1);
        }
        public override void MoveLeft()
        {
            CheckIfFinish(x - 1, y);
            TryRelocate(x - 1, y);
        }
        public override void MoveRight()
        {
            CheckIfFinish(x + 1, y);
            TryRelocate(x + 1, y);
        }
        public void Kill()
        {
            disposed = true;
        }
        public bool IsAlive()
        {
            return !disposed;
        }
        public bool IsFinished()
        {
            return finished;
        }
     
    }
}
