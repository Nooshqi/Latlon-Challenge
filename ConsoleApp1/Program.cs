using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results = new string[50]; 
        static string output;
        static char key;
        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {   
            //Declare single instance of JsonFeed
            JsonFeed jsonfd = new JsonFeed("https://api.chucknorris.io/jokes/");


            printer.Print("Welcome. Press '?' followed by ENTER to get started. Press any key to leave");
            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    printer.Print("Press C to get JOKE CATEGORIES or Press R to get RANDOM JOKES. Q to QUIT");
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        output = jsonfd.GetCategories();
                        printer.Print(output);
                    }
                    else if (key == 'r')
                    {
                        printer.Print('\n'+"Press Y if you want a RANDOM NAME else we'll stick to CHUCK NORRIS");
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                        {
                            jsonfd.Getnames();
                        }
                        printer.Print('\n'+"How many jokes do you want? (1-9) then press ENTER. Wrong entries default to 1");
                        int n;

                        if (int.TryParse(Console.ReadLine(), out n)){
                            n = (n<1||n>9)?1:n*1;
                        }
                        else{
                            printer.Print("Wrong input. We'll stick to one joke");
                            n=1;
                        }

                        printer.Print("Press Y if you want to specify a CATEGORY else we'll leave it unspecified");
                        GetEnteredKey(Console.ReadKey());
                        if (key == 'y')
                        {   
                            printer.Print('\n'+"Enter a category;");
                            results = jsonfd.GetRandomJokes(Console.ReadLine(), n);
                            printer.Print(results);
                        }
                        else
                        {
                            results = jsonfd.GetRandomJokes(null, n);
                            printer.Print(results);
                        }
                    }
                    else if (key == 'q')
                    {
                        break;
                    }
                    else printer.Print('\n'+"Sorry, Wrong Input");
                }
            }
            else printer.Print("Goodbye");

        }

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                default:
                    key = 'q';
                    break;
            }
        }
    }
}
