# Feedback

## Project

The code you created to randomly generate Chuck Norris jokes is a good start. Unfortunately it felt unfinished and not tested.

When trying to run the code at the begining, I could not get it working. I had to look into the code to realize some of the api calls were not hitting the right endpoint.

The fact that the project was under the `ConsoleApp1` folder also made it feel like it is unfinished or a test project.

## `Program.cs`

I thought it was a good idea to have the flow of the program in this `Program` class. Any new developers to this program should look at this first to get an idea of what is happening. 
The flow between the questions displayed to the user could have been made more clear. Furthermore, I found that the way the user is prompted for input was inconsistent, which 
could lead to some unexpected errors when updating the code.

From the user's point of view, it is quite confusing in terms of what is expected for the input. Sometimes you only need to press a single key, and sometimes you need to enter a value and then press enter. This should be made more clear. 
Additionally, the user inputs kept switching between needing to press enter or not. Additionally, you should always validate the user's input. They cannot be expected to always enter an integer even though that is what the prompt says. 
In the case of asking the user for the number of jokes they want, there is no validation if they entered a number, a positive number, or even a number between 1 and 9. Another example is the `GetEnteredKey` function. 
This function had no code to handle if some other unexpected key got pressed instead.

Even if there is user input validation, there should always be some code catching exceptions. Nowhere could I find a try catch statement. If an unexpected error occurs, the result would be bad user experience.

From a developer's point of view, the `results` attribute is confusing and could lead to complications when adding new features. You should not use this variable for more than one purpose, else it leads to confusion.
You should have one variable per purpose. If you use a single variable for multiple purposes, then you might encounter a case where you need this variable from at least 2 different functions and both write to it.
This introduces a race condition which are hard to detect and, in this case, could be easily avoided.

## `ConsolePrinter.cs`

From my understanding when looking at the code, the only way to print anything to the console was to do: 
```
printer.Value("Message").ToString()
```

This is 2 function calls to simply write to the conole. One function was never called without the other, so you could have simplified the 2 functions into a single one.

As I previously mentioned, the way you requested input from the user was inconsistent, you could add some methods to this class to handle the user input in a more consistent way.

## `JsonFeed.cs`

The constructor of the `JsonFeed` class had an argument called `results`, but this was not being used. Additionally, the only documentation in the whole project was for the method `Getnames` and it was not even accurate. 
The documentation mentions an argument not present in the code. There was also no description of the return value.

You wrote a different function for each API call, but they could have been generalized to a single method that would handle any API GET request.

The `GetRandomJokes` method required a first and last name to replace Chuck Norris' name, but there is no need for that. You could have simplified this by requiring a single name and use that instead. It would have been easier.

In all of the methods calling their individual APIs, there is a lack of URL encoding. This is specifically needed in the `GetRandomJokes` method where a category can be passed in. The category could contain forbidden characters and lead to errors.

The file overall had some inconsistent indentations, this could lead to misreading the code.

## Unit Tests

A lot of the coding errors could have been prevented by having some unit tests written. It would have helped you structure your code and find errors within your code.

I have written some unit tests for you, but I did not have enough time to look into how to mock the GET requests and user inputs.