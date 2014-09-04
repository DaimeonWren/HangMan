using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangMan
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run our game function
            HangMan();

            //Keep the console open
            Console.ReadKey();

        }

        //Our guess limit
        static int guesses = 7;

        //word to guess variable
        static string wordToguess = string.Empty;

        //The letters guessed
        static string lettersGuessed = string.Empty;

        //List of words to choose from!!!
        static List<string> wordBank = new List<string>() {"Distraught", "Manifest", "Dislocation", "Fish", "Loser", "Growth", "intellectual"};

        //Players name
        static string playerName = string.Empty;

        //Win boolean!
        static bool won = false;


        /// <summary>
        /// The function of all functions!
        /// </summary>
        static void HangMan()
        {
            //Prompt for name
            Console.WriteLine("What is your name?");

            //Get that input
            playerName = Console.ReadLine();

            //Read them the rules
            ReadRules(playerName);

            //Set our word to guess
            wordToguess = GetWord();

            //Main game loop!
            while (!won && guesses > 0)
            {
                //Display the game
                Display();

                //Have them guess
                guess();

            }

            //If they won
            if (won)
            {
                //Compliment thier intellect
                Console.WriteLine("Nice guessing, NON-Loser!");
            }
            //Otherwise
            else
            {
                //Insult severely
                Console.WriteLine("Let it be known that " + playerName + " is a true DUNCE!");
            }

        }


        /// <summary>
        /// Displays the rules of the game...
        /// </summary>
        /// <param name="name"></param>
        static void ReadRules(string name)
        {
            //Writes the rules and greets the player
            Console.WriteLine("Hello, " + name + " welcome to hangman.\nYou have seven guesses to discover a word chosen at random.\nYou may guess the whole word or a single letter.\nGood Luck!");
        }

        /// <summary>
        /// Sets the word to guess
        /// </summary>
        /// <returns></returns>
        static string GetWord()
        {
            //Empty word for storing stuff
            string word = string.Empty;

            //A random thing of randomyness
            Random rng = new Random();

            //Set our word to a random word in our word bank
            word = wordBank[rng.Next(0, wordBank.Count)];

            //Return the word it hath chosen
            return word;
        }


        /// <summary>
        /// Displays the relevent game info
        /// </summary>
        static void Display()
        {

            Console.WriteLine(MaskedWord() + "\nLetters Guessed: " + lettersGuessed + "\nGuesses Left: " + guesses);

        }

        /// <summary>
        /// Masks the word to guess
        /// </summary>
        /// <returns>A masked word</returns>
        static string MaskedWord()
        {

            string mWord = string.Empty;


            foreach (var letter in wordToguess)
            {

                if (lettersGuessed.ToLower().Contains(letter.ToString().ToLower()))
                {
                    mWord += letter + " ";
                }
                else
                {
                    mWord += "_" + " ";
                }


            }

            return mWord;


        }

        /// <summary>
        /// Takes their guess and evaluates it for validity
        /// </summary>
        static void guess ()
        {
            
            //Prompt user for a guess
            Console.WriteLine("Take a swing");

            //Get that input
            string guess = Console.ReadLine();

            //If it's longer than a letter
            if (guess.Length > 1)
            {
                //Check against the whole word
                if (guess.ToLower() == wordToguess.ToLower())
                {
                    //If it was correct congratulate them
                    Console.WriteLine("Good work!");
                    //They won
                    won = true;
                }
                //They were wrong!
                else
                {
                    //Steal their guess
                    guesses--;

                    //Shame them
                    Console.WriteLine("You didn't really think that was it did you?");
                }
            }
            //They took the letter by letter route
            else
            {
                //See if they already guessed this one
                if (!(lettersGuessed.ToLower().Contains(guess.ToLower())))
                {

                    //Add guess to the guessed letters
                    lettersGuessed += guess;

                    //If it was correct
                    if (wordToguess.ToLower().Contains(guess.ToLower()))
                    {
                        //Be confused
                        Console.WriteLine("Nice try,but, oh wait, nevermind");

                        //Set winning to true if all letters were guessed
                        won = AllLetters();
                    }
                    //They are bad at this game
                    else
                    {
                        //Take a guess
                        guesses--;
                        //Berate them
                        Console.WriteLine("That was a horrible guess");

                    }

                }
            }




        }

        /// <summary>
        /// Checks to see if all letters have been guessed in the word to guess
        /// </summary>
        /// <returns></returns>
        static bool AllLetters()
        {
            //Declare a bool to return
            bool isTrue = false;

            //Loop thru the masked word
            foreach (var letter in MaskedWord())
            {
                //If there was an underscore they need to guess some more
                if (letter == '_')
                {
                    //set the return to false
                    isTrue = false;
                    //Leave the loop
                    break;
                }

                //This letter is guessed
                else
                {
                    //Set our return to true until proven otherwise
                    isTrue = true;

                }

            }

            //Return our evaluation
            return isTrue;
        }
    }
}
