using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    public abstract class BusBoardDisplay
    {
        public abstract void DisplayWelcome();
        public abstract void DisplayError(string message);
        public abstract void DisplayMessage(string message);
        public abstract void DisplayBuses(List<BusInfo> busList, int number);
    }
}