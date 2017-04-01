using System;
using System.Collections.Generic;
using System.Text;

namespace KekMan
{
    abstract class Creature
    {
        protected char[,] gameField;
        protected int x;
        protected int y;
        protected char currentSymbol='-';

        protected abstract bool CanMoveTo(int x, int y);
        protected void TryRelocate(int x, int y)
        {
            if (CanMoveTo(x, y))
            {
                gameField[this.x, this.y] = GameFieldManager.emptySymbol;
                gameField[x, y] = currentSymbol;
                this.x = x;
                this.y = y;
            }
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public abstract void MoveUp();
        public abstract void MoveDown();
        public abstract void MoveLeft();
        public abstract void MoveRight();
        public Creature(char[,] gameField, int x, int y)
        {
            this.gameField = gameField;
            this.x = x;
            this.y = y;
        }

    }
}
