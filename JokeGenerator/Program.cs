using System;

namespace JokeGenerator
{
    /// <summary>
    /// This class runs the main program of the joke generator.
    /// It decides the actions to take for eeach state.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main function of the program
        /// </summary>
        static void Main()
        {
            try
            {
                JokeGenerator generator = new JokeGenerator();

                while (generator.state != JokeGenerator.State.Cancel)
                {
                    switch (generator.state)
                    {
                        case JokeGenerator.State.Main:
                            generator.DisplayMainInformation();
                            generator.GetNextState();
                            break;
                        case JokeGenerator.State.Name:
                            generator.GetName();
                            generator.ResetState();
                            break;
                        case JokeGenerator.State.RandomName:
                            generator.GetRandomName();
                            generator.ResetState();
                            break;
                        case JokeGenerator.State.Category:
                            generator.GetCategory();
                            generator.ResetState();
                            break;
                        case JokeGenerator.State.Number:
                            generator.GetNumber();
                            generator.ResetState();
                            break;
                        case JokeGenerator.State.Joke:
                            generator.DisplayJokes();
                            generator.ResetState();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an unexpected error:");
                Console.WriteLine(e.GetType());
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
