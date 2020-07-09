using System;

namespace BusBoard.ConsoleApp
{
    public class ConsoleBusBoardInput : BusBoardInput
    {
        public override string GetStringInput()
        {
            Console.Write("\nPlease enter a string:\n> ");
            return Console.ReadLine();
        }
    }
}