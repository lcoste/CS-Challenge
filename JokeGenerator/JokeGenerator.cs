using System;
using System.Collections.Generic;
using System.Linq;

namespace JokeGenerator
{
    /// <summary>
    /// This class handles the joke generation according to parameters.
    /// </summary>
    public class JokeGenerator
    {        
        /// <summary>
        /// Enumeration of all states handled by the joke generator.
        /// </summary>
        public enum State
        {
            Main,
            Joke,
            Name,
            RandomName,
            Category,
            Number,
            Cancel
        }

        // Parameters needed to generate the requested set of jokes.
        public State state = State.Main;
        private string name = null;
        private string category = null;
        private int number = 1;
        readonly string[] categories;

        readonly ConsoleInterface console = new ConsoleInterface();
        readonly JsonFeed feed = new JsonFeed();

        /// <summary>
        /// Initializer of the JokeGenerator.
        /// Sets up the list of available categories.
        /// </summary>
        public JokeGenerator()
        {
            string result = feed.Get(JsonFeed.CHUCK_NORRIS_API, "categories");

            result = result.Replace("\"", "");
            result = result.Replace("[", "");
            result = result.Replace("]", "");

            categories = result.Split(',');
            categories = categories.Append("None").ToArray();
        }

        /// <summary>
        /// Displays the joke parameters.
        /// Will display the default values if some values have not been provided by the user.
        /// </summary>
        public void DisplayMainInformation()
        {
            console.ClearScreen();
            console.Print("Your joke parameters:\n");

            console.Print(string.Format("Name: {0}", name ?? "Chuck Norris"));
            console.Print(string.Format("Category: {0}", category ?? "None"));
            console.Print(string.Format("Number of jokes: {0}\n", number));
        }

        /// <summary>
        /// Shows and prompts users for the next step to take.
        /// </summary>
        public void GetNextState()
        {
            console.Print("Press one of the following keys: ");
            console.Print("    - <Enter>: To generate your joke" + (number > 1 ? "s" : ""));
            console.Print("    - N: To provide the name of your choice");
            console.Print("    - R: To generate a random name");
            console.Print("    - C: To select a category");
            console.Print("    - A: To choose the amount of jokes you wish for");
            console.Print("    - <Esc>: To cancel and get no joke");
            console.Print("");

            bool restart = true;

            while (restart)
            {
                ConsoleKeyInfo key = console.WaitForKeyPress();
                console.Print("");
                restart = false;

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        state = State.Joke;
                        break;
                    case ConsoleKey.N:
                        state = State.Name;
                        break;
                    case ConsoleKey.R:
                        state = State.RandomName;
                        break;
                    case ConsoleKey.C:
                        state = State.Category;
                        break;
                    case ConsoleKey.A:
                        state = State.Number;
                        break;
                    case ConsoleKey.Escape:
                        state = State.Cancel;
                        break;
                    default:
                        console.Print("This is not a supported key. Please provide a valid key.");
                        restart = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Resets the program's state to it's default state: <c>Main</c>.
        /// </summary>
        public void ResetState()
        {
            state = State.Main;
        }

        /// <summary>
        /// Prompts user for a name to use in the jokes.
        /// </summary>
        public void GetName()
        {
            name = console.Input("What name do you wish to use instead (press <Enter> to submit your value): ", true);
        }

        /// <summary>
        /// Fetches from an API a random name to use in the jokes.
        /// </summary>
        public void GetRandomName()
        {
            dynamic result = feed.Get(JsonFeed.NAME_API, new string[] { "name", "surname" });
            name = result["name"].ToString() + ' ' + result["surname"].ToString();
        }

        /// <summary>
        /// Displays the available categories and prompts user to select one.
        /// Allows user to type <c>None</c> to unset the category selection.
        /// </summary>
        public void GetCategory()
        {
            console.Print("Available categories:");
            console.Print(categories);

            string choice = console.Input("Please pick one of the categories (press <Enter> to submit your value): ", categories);

            category = String.Compare(choice, "None", StringComparison.OrdinalIgnoreCase) != 0 ? choice : null;
        }

        /// <summary>
        /// Prompts user for the number of jokes desired.
        /// </summary>
        public void GetNumber()
        {
            number = console.InputInt("Please type in the number of jokes you wish to get (press <Enter> to submit your value): ");
        }

        /// <summary>
        /// Fetches from an API Chuck Norris jokes.
        /// Hits the API as many times as necessary to get all the jokes required.
        /// Uses <c>category</c> to get the right category of jokes.
        /// If there is a preferred name then it replaces any mention of Chuck Norris with the v name.
        /// </summary>
        public void DisplayJokes()
        {
            console.ClearScreen();
            console.Print("Generating jokes...");
            string[] jokes = new string[number];
            string joke;
            dynamic result;

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "category", category }
            };

            for (int i = 0; i < number; i++)
            {
                console.Print(String.Format("Generating joke {0} of {1}", i + 1, number));
                result = feed.Get(JsonFeed.CHUCK_NORRIS_API, "random", category != null ? parameters : null, new string[] { "value" });

                joke = result["value"].ToString();
                if (name != null)
                    joke = joke.Replace("Chuck Norris", name);

                jokes[i] = joke;
            }

            console.ClearScreen();
            console.Print("Jokes:");
            console.Print(jokes);

            console.Print("\n\nPress any key to go back to the main screen...");
            console.WaitForKeyPress();
        }
    }
}
