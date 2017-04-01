using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KekMan
{
    class Menu
    {
        GameFieldManager gameFieldManager;
        public void UseMenu()
        {
            Console.Clear();
            int levelsCount = CheckLevelsFiles();
            int chosedLevel;
            try
            {
                chosedLevel = ReadChosedLevel(levelsCount);
            }
            catch
            {
                OnWrongLevelChosed(levelsCount);
                return;
            }
            try
            {
                string path = string.Format(Environment.CurrentDirectory + "/level_{0}.txt", chosedLevel);
                string levelStr = File.ReadAllText(path);
                string levelMap=levelStr.Substring(levelStr.IndexOf('\n'));
                string moveDelay = levelStr.Remove(levelStr.IndexOf('\n'));
                GameFieldManager.moveDelay = Convert.ToInt32(moveDelay);
                gameFieldManager = new GameFieldManager(levelMap);
                Console.Clear();
                bool gameRes = gameFieldManager.StartGame();
                Console.Clear();
                OnGameResult(gameRes);
            }
            catch (Exception e)
            {
                OnLevelOpenException(e);
            }
        }
        int CheckLevelsFiles()//возвращает количество файлов с уровнями
        {
            string path = Environment.CurrentDirectory + "/level_";
            int count = 1;
            while (File.Exists(string.Format("{0}{1}.txt", path, count)))
            {
                count++;
            }
            if (count <= 1)
                throw new Exception("Havn`t levels files!");

            return count-1;

        }
        int ReadChosedLevel(int levelsCount)
        {
            Console.WriteLine("There is  " + levelsCount.ToString() + ", and you can chose one of them.");
            Console.Write("Enter level №: ");
            return Convert.ToInt32( Console.ReadLine());
        }
        void OnWrongLevelChosed(int levelsCount)
        {
            Console.WriteLine("You entered wrong level №, please enter level from 1 to " + levelsCount.ToString() + ".");
            Console.ReadLine();
            UseMenu();
        }
        void OnLevelOpenException(Exception exeption)
        {
            Console.WriteLine("The level can't be opened, there is no level file or file have wrong map.");
            Console.ReadLine();
            UseMenu();
        }
        void OnGameResult(bool levelCompleted)
        {
            if (levelCompleted)
            {
                Console.WriteLine("You WON, congratulations!");
            }
            else
            {
                Console.WriteLine("We are so sorry for creation so hard level, you can restart if you want to rematch.");
            }
            Console.ReadLine();
            gameFieldManager.Dispose();
            gameFieldManager = null;
            UseMenu();
        }
    }
}
