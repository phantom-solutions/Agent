using System;

namespace Borealis_Server_Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            string ControlPanel_IPAddress = null;
            Console.WriteLine("Initializing First-Time Launch of Borealis Server Agent...");
            Console.WriteLine("Please Enter IP Address of Control Panel:");
            ControlPanel_IPAddress = Console.ReadLine();

            Console.WriteLine("You entered:{0}, press ENTER to confirm.", ControlPanel_IPAddress);
            Console.ReadLine(); //Pause
        }
    }
}
