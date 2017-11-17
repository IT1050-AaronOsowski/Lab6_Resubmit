using System;
using System.Threading;
using System.Collections.Generic;

namespace Lab6
{
    class Program
    {
        static ConsoleColor mainColor = ConsoleColor.DarkGreen;
        static ConsoleColor secondaryColor = ConsoleColor.DarkRed;


        static void Main(string[] args)
        {


            #region questionOne
            //find leap years with the built-in C# method
            Console.ForegroundColor = ConsoleColor.Red;
            LeapYearsDateTime(20);

            Console.Write("\n\n");

            //Compare C# DateTime results to my own work
            Console.ForegroundColor = ConsoleColor.Cyan;
            LeapYearsCustom(20);

            //Pause the screen
            string endMessage = "Press a key";
            Console.ForegroundColor = ConsoleColor.Yellow;
            int n = 0;

            while (true)
            {
                if (n >= endMessage.Length)
                {
                    n = 0;
                    Console.Clear();
                    //find leap years with the built-in C# method
                    Console.ForegroundColor = ConsoleColor.Red;
                    LeapYearsDateTime(20);

                    Console.Write("\n\n");

                    //Compare C# DateTime results to my own work
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    LeapYearsCustom(20);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write(endMessage[n]);
                Thread.Sleep(1000 / endMessage.Length);
                n++;

                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            #endregion

            #region questionTwo

            GuessingGame();

            #endregion

            #region questionThree

            //Intro to the Grocery Tracker
            Console.ForegroundColor = mainColor;
            Console.WriteLine("Hello human worm-baby! Welcome to the grocery list tracker!" +
                "\n\nEnter an item when it says \"Item >\"." +
                "\n\nEnter a price when it says \"Price > \"." +
                "\n\nEnter \"E\" at any time to exit and view your horrible list!" +
                               "\n\nPress enter to start.");

            Console.ReadLine();

            GroceryListTracker();

            #endregion

        }

        //Loop for grocery list tracker
        static void GroceryListTracker()
        {
            ListState ls = ListState.Item;
            string listItem = "";
            double listPrice = 0.0;
            Dictionary<string, double> groceryList = new Dictionary<string, double>();
            bool exit = false;
            double listTotalCost = 0.0;

            do
            {


                Console.Clear();

                for (int n = 0; n < 2; n++)
                {
                    if (exit)
                    {
                        break;
                    }
                    Console.ForegroundColor = secondaryColor;
                    Console.Write("Enter a{0} {1} >  ", ls == ListState.Item ? "n" : "", ls.ToString());

                    if (!exit && ls == ListState.Item)
                    {
                        string input = Console.ReadLine().Trim().ToLower();
                        if (input == "e" || input == "E")
                        {
                            exit = true;
                        }
                        else
                        {
                            listItem = input;
                            ls = ListState.Price;
                        }
                    }
                    else if (!exit && ls == ListState.Price)
                    {
                        while (true)
                        {
                            try
                            {
                                listPrice = GetDoubleInput(out exit);
                                ls = ListState.Item;
                                listTotalCost += listPrice;
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Price was not entered as a number.\nTry again.");
                                Console.ReadKey(true);
                                Console.Clear();
                                Console.Write("Enter a{0} {1} >  ", ls == ListState.Item ? "n" : "", ls.ToString());
                            }
                        }

                    }
                }

                if (!exit)
                {
                    try
                    {
                        groceryList.Add(listItem, listPrice);
                    }
                    catch
                    {

                    }
                }

            } while (!exit);


            Console.Clear();
            Console.WriteLine("Here is your list; press a key to exit.\n\n");

            Console.ForegroundColor = mainColor;
            foreach (KeyValuePair<string, double> v in groceryList)
            {
                Console.WriteLine(v);

            }
            Console.Write("\nTotal Cost: {0}", listTotalCost);
            Console.ReadKey(true);

        }

        enum ListState
        {

            Item, Price

        }

        static double GetDoubleInput(out bool exit)
        {
            string input = Console.ReadLine().Trim().ToLower();
            if (input == "e" || input == "E")
            {
                exit = true;
                return 0.0;
            }
            else
            {
                exit = false;
                return Convert.ToDouble(input);
            }


        }

        //Guessing Game. Guess a #. Computer tells you
        //if it is high or low.
        static void GuessingGame()
        {
            //Game variables
            int userGuess = 0;
            int tries = 0;
            int totalTries = 0;
            int death = 4;
            int previousGuess = 0;

            //Generate a random number
            Random r = new Random();
            int numberToGuess = r.Next(100);

            //Explain the game
            Console.WriteLine("You are imprisoned by Germans." +
                "\nGuess the number correctly, and they will release you.");
            Console.Write("\nPress a key.");
            Console.ReadKey(true);


            do
            {

                try
                {
                    //Change color for error message
                    Console.ForegroundColor = secondaryColor;
                    userGuess = GetIntegerInput("\n\n> SCHNELL! SCHNELL!\n\n" +
                        "[previous guess: " + previousGuess + "]");
                    previousGuess = userGuess;

                    Console.Clear();

                    if (userGuess > numberToGuess)
                    {
                        Console.Write("\n\n> ZU HOCH!");

                    }
                    else if (userGuess < numberToGuess)
                    {
                        Console.Write("\n\n> ZU NIEDRIG!");
                    }

                    //Change color back
                    Console.ForegroundColor = mainColor;

                    if (userGuess != numberToGuess)
                    {
                        Console.Write("\n\nBlast!! If only you understood German...\nTry again..");
                        Console.ReadKey(true);
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("> " + e.Message);
                    tries++;

                    //Change color back
                    Console.ForegroundColor = mainColor;

                    if (tries < death)
                    {
                        Console.Write("\n\nYou have angered the Germans.\nTry again..");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("You have angered the Germans once too many." +
                            "\nYou are dead.\n\nPress a key to try again.");
                        Console.ReadKey(true);
                        tries = 0;
                    }
                }
                finally
                {
                    totalTries++;
                }

            } while (userGuess != numberToGuess);

            Console.Clear();

            Console.WriteLine("Congratulations. After much deliberation, the Germans set you free." +
                "\n\nIt took you {0} tries", totalTries);
            Console.ReadKey(true);

        }

        static int GetIntegerInput(string message)
        {
            int returnValue = 0;
            Console.WriteLine(message);

            if (!int.TryParse(Console.ReadLine().Trim().ToLower(), out returnValue))
            {
                throw new Exception("NEIN!");

            }

            return returnValue;
        }

        //Get the leap years using multiple if statements
        static void LeapYearsCustom(int numberofYears)
        {
            int counter = 0;
            DateTime d = DateTime.Today;
            int year = d.Year;

            while (counter < numberofYears)
            {
                //Set the default string to blank
                string message = "";

                if (year % 4 == 0)
                {
                    if (year % 100 != 0 || year % 100 == 0 && year % 400 == 0)
                    {
                        message = year + " is a leap year!\n";
                    }

                }

                Console.Write(message);

                counter++;
                year = d.Year + counter;
            }
        }

        //Get the leap years using the built-in method
        static void LeapYearsDateTime(int numberOfYears)
        {
            //Get the current year
            DateTime d = DateTime.Today;
            int year = d.Year;

            //counter so the loop stops after X years
            int counter = 0;
            while (counter++ < numberOfYears)
            {
                if (DateTime.IsLeapYear(year + counter))
                {
                    Console.WriteLine("{0} is a leap year!", year + counter);
                }

                counter++;
            }
        }
    }
}
