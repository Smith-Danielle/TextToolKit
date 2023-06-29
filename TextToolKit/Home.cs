using System;
using System.Linq;
namespace TextToolKit
{
    public class Home
    {
        public Home()
        {
        }

        public string InitialText;
        public string LatestText;

        public void RunTools()
        {
            Console.WriteLine("Welcome to *Text Tool Kit*");
            Console.WriteLine("This application allows you to search, transform, and get a detailed anaylsis on your submitted text.");

            int confirm = 0;
            while (confirm != 1)
            {
                Console.WriteLine("Please enter your text to get started");
                InitialText = Console.ReadLine();
                while (InitialText.Length == 0 || InitialText.All(x => x == ' '))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Entry");
                    Console.WriteLine("Your entry can not be empty and must contain at least one letter, number, or special character.");
                    Console.WriteLine("Please enter your text to get started.");
                    InitialText = Console.ReadLine();
                }

                Console.Clear();
                Console.WriteLine("Here is the text you entered:");
                Console.WriteLine(InitialText);
                Console.WriteLine("Please enter a number from the options below to proceed.");
                Console.WriteLine("1 - Confirm and continue with displayed text.");
                Console.WriteLine("2 - Resubmit text.");
                Console.WriteLine("3 - Quit and Exit.");

                bool validConfirm = int.TryParse(Console.ReadLine(), out confirm);
                while (confirm != 1 || confirm != 2 || confirm != 3)
                {
                    validConfirm = int.TryParse(Console.ReadLine(), out confirm);
                }
            }

            if (confirm == 3)
            {
                Console.WriteLine("Thank you for using *Text Tool Kit*");
            }
            else
            {
                ToolPicker();
            }
        }

        public void ToolPicker()
        {
            Console.Clear();
            Console.WriteLine(InitialText);
            Console.WriteLine("Please enter a number from the options below to proceed.");
            Console.WriteLine("1 - Analyze");
            Console.WriteLine("Run a detailed analysis.");
            Console.WriteLine("2 - Search");
            Console.WriteLine("Search and count specific occurences with in your text.");
            Console.WriteLine("3 - Transform");
            Console.WriteLine("Select from a variety of tools to alter your text.");
            Console.WriteLine("4 - Quit and Exit.");

            int pick = 0;
            bool validPick = int.TryParse(Console.ReadLine(), out pick);
            while (pick != 1 || pick != 2 || pick != 3 || pick != 4)
            {
                validPick = int.TryParse(Console.ReadLine(), out pick);
            }

            if (pick == 1)
            {
                Analyze analyze = new Analyze();
                analyze.CreateAnalysis(InitialText);
            }

        }
    }
}
