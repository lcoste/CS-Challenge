using System;
using System.Linq;

namespace JokeGenerator
{
    /// <summary>
    /// Handles all interaction with the user.
    /// </summary>
    public class ConsoleInterface
    {
        /// <summary>
        /// Clears the console screen.
        /// </summary>
        public void ClearScreen()
        {
            System.Console.Clear();
        }

        /// <summary>
        /// Prints the message passed in onto the console.
        /// </summary>
        /// <param name="value">Message to print on the conole.</param>
        public void Print(string value)
        {
            Console.WriteLine(value);
        }

        /// <summary>
        /// Prints a list of values to the console.
        /// </summary>
        /// <param name="values">Values to list onto the console.</param>
        public void Print(string[] values)
        {
            Console.Write("- ");
            Print(string.Join("\n- ", values));
        }

        /// <summary>
        /// Waits for user to press a key.
        /// </summary>
        /// <returns>ConsoleKeyInfo of the key pressed by the user.</returns>
        public ConsoleKeyInfo WaitForKeyPress()
        {
            return Console.ReadKey();
        }

        /// <summary>
        /// Prompts user with a message and listens to the value given by the user.
        /// </summary>
        /// <param name="message">Message to display to the user before listenning for their input.</param>
        /// <returns>Value returned by the user.</returns>
        public string Input(string message)
        {
            Console.Write(message);

            string result = Console.ReadLine();

            return result;
        }

        /// <summary>
        /// Prompts user for an integer.
        /// </summary>
        /// <param name="message">Message to display to the user.</param>
        /// <returns>Positive integer provided by the user.</returns>
        public int InputInt(string message)
        {
            string result = Input(message); ;
            int number;

            while (!Int32.TryParse(result, out number) || number <= 0)
            {
                Print("Please provide a valid number of jokes");
                result = Input(message);
            }

            return number;
        }

        /// <summary>
        /// Prompts user for an input.
        /// </summary>
        /// <param name="message">Message to display to user.</param>
        /// <param name="inputRequired">Boolean indicating if a value must be returned.</param>
        /// <returns>Value returned by the user.</returns>
        public string Input(string message, bool inputRequired)
        {
            string result = Input(message); ;

            while (inputRequired && result.Length == 0)
            {
                Print("Value is required. Please make sure to type something.");
                result = Input(message);
            }

            return result;
        }

        /// <summary>
        /// Prompts user for an input.
        /// </summary>
        /// <param name="message">Message to display to user.</param>
        /// <param name="choices">List of values that user must pick from.</param>
        /// <returns>Value returned by the user.</returns>
        public string Input(string message, string[] choices)
        {
            string result = Input(message);

            while (!choices.Contains(result, StringComparer.OrdinalIgnoreCase))
            {
                Print("This value is not acceptable, it is not part of the available choices.");
                result = Input(message);
            }

            return result;
        }
    }
}
