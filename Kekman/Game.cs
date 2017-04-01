using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace KekMan
{
    class Game
    {
        static void Main(string[] args)
        {
            WindowMaximaizer.Maximize();
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.Title = @"KekMan";
            Console.WriteLine("Hello dear player, i'm not happy that you are playing this game, please click red X in a high right corner, good bye.\nPress Enter to continue.");
            Console.ReadLine();
            Console.WriteLine("If you still wanna continue play this game here is your controls : \nW - move up,\nS - move down,\nA - move left,\nD - move right.");
            Console.WriteLine(string.Format("The goal of this game is to reach '{0}'. Enter to continue.", GameFieldManager.finishSymbol));
            Console.ReadLine();
            Menu menu = new Menu();
            menu.UseMenu();
            Console.ReadLine();
        }
    }

    class WindowMaximaizer
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
        public static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }
    }
}
