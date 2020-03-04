using System;
using static System.Console;

namespace MSGraph_FirstApp
{
    public class Program
    {
        public static void Main()
        {
            WriteLine($".NET Core Graph First App{Environment.NewLine}");


            var isValidChoice = int.TryParse(ReadLine(), out int choice);
            if(!isValidChoice)
            {
                //Invalid options choosing one
                choice = 1;
            }

            var message = choice switch
            {
                0 => "Goodbye...",
                1 => "<Place Holder for Access Token",
                2 => "<Place Holder for Calendar List",
                _ => "Invalid Choice!, Please Try Again"
            };

            WriteLine(message);
        }
    }
}
