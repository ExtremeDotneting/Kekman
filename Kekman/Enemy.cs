using System;
using System.Collections.Generic;
using System.Text;

namespace KekMan
{
    class Enemy : Creature
    {
        public Enemy(char[,] gameField, int x, int y) : base(gameField,x,y)
        {
            currentSymbol = GameFieldManager.enemySymbol;
        }
        public bool MoveToPlayer(int playerX, int playerY)
        {
            int up = playerY - y;
            int down = -playerY + y;
            int left = playerX - x;
            int right = -playerX + x;
            int max=Math.Max(right, Math.Max(left, Math.Max(up, down)));
            if (max == up)
                MoveDown();
            else if (max == down)
                MoveUp();
            else if (max == left)
                MoveRight();
            else
                MoveLeft();
            if (max==1)
                return true;
            return false;
        }
        protected override bool CanMoveTo(int x, int y)
        {
            return x >= 0 && y >= 0 && x < gameField.GetLength(0) && y < gameField.GetLength(1) && (gameField[x, y] == GameFieldManager.emptySymbol || gameField[x, y] == GameFieldManager.wallSymbol);
        }
        public override void MoveUp()
        {
            TryRelocate(x, y - 1);
        }
        public override void MoveDown()
        {
            TryRelocate(x, y + 1);
        }
        public override void MoveLeft()
        {
            TryRelocate(x - 1, y);
        }
        public override void MoveRight()
        {
            TryRelocate(x + 1, y);
        }
    }
}
