using System;
using System.Diagnostics;
using System.Threading;

namespace HashtableGame
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework hashtables!");
            var game = new HashtableGame(88);
            while(true)
            {
                try
                {
                    game.Execute();
                } catch {
                    Console.WriteLine("failed to execute game");
                }
                Thread.Sleep(50);
            }
        }
    }
}
