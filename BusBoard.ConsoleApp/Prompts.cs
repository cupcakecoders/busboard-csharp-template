using System;

namespace BusBoard
{
    public class Prompts
    {
        public string GetUserInput(string prompt)
        {
            Console.WriteLine(prompt);
            string userRequest = Console.ReadLine();
            return userRequest;
        }
    }
}