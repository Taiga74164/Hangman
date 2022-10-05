using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        public static string wordToGuess;
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================\n\t\tHangman\n==========================================");
            HangmanGame();
            Console.Read();
        }

        static void HangmanGame()
        {
            //for bonus, have an array of words to choose from for the wordToGuess
            string[] wordArrayStrings = { "reflect", "report", "cancel", "cook", "initiate", "creative", "file", "bear", "adopt", "accept", "smoke", "live", "measure", "receive", "arrest", "comprise", "interpret", "peer", "laugh", "provide" };
            
            //bonus: figure out a way to keep track of letters guessed before
            List<string> stringList = new List<string>(wordArrayStrings);

        startover:

            //new Random().Next(0, arrayLength - 1); this code gives you a random integer between 0 and the last element in a particular array    
            Random rand = new Random();
            wordToGuess = stringList[rand.Next(0, stringList.Count - 1)];

            //foreach (var i in stringList)
            //    Console.WriteLine(i);

            int numMistakes = wordToGuess.Length;
            int numWrongGuesses = 0;

            bool gameOver = false;
            //let's think about the hangman algorithm
            int wordLength = wordToGuess.Length; //this gives us the length of our word
            char[] enteredWord = new char[wordLength];

            for (int j = 0; j < wordToGuess.Length; j++)
                enteredWord[j] = '_';

            while (!gameOver)
            {
                Console.WriteLine("Enter in a letter");
                char readInLetter = Console.ReadLine().ToLower()[0];

                //we need to check if that letter is in the word we are guessing
                if (!wordToGuess.Contains(readInLetter))
                {
                    numWrongGuesses++;
                    Console.WriteLine($"Word is wrong! You have {numMistakes - numWrongGuesses} guesses left. \n");
                }
                else
                {
                    Console.WriteLine("Word is correct!\n");
                }

                //we are going to need to figure out where that letter appears in the word, and fill it in the enteredWord
                for (int k = 0; wordToGuess.Length > k; k++)
                    if (readInLetter == wordToGuess[k])
                        enteredWord[k] = readInLetter;


                //before we exit this loop, we need to make sure to set whether we won or lost and display that to the user
                string currentWord = new string(enteredWord);
                if (currentWord == wordToGuess)
                {
                    Console.WriteLine($"You Won! The final word is {currentWord}");
                    break;
                }
                else if (numWrongGuesses == numMistakes)
                {
                    Console.WriteLine("Game Over!!");
                    break;
                }

                //at the end of each loop, i want you to call the DisplayCurrentWord function with our entered word
                DisplayCurrentWord(enteredWord);
            }

            
            string finalWord = new string(enteredWord); //this is for the final word
            if (finalWord == wordToGuess)
            {
                stringList.Remove(finalWord);
            }

            Console.WriteLine("Would you like to play again? (Y/N)");
            var input = Console.ReadLine().ToLower()[0];
            if (input == 'y')
            {
                goto startover;
            }
            else
            {
                Console.WriteLine("Have a great day!");
            }
        }
        static void DisplayCurrentWord(char[] enteredWord)
        {
            string currentWord = new string(enteredWord);
            //what do we need to do here to display the entered word?
            Console.WriteLine(currentWord == wordToGuess ? "\n" : currentWord);
        }
    }
}