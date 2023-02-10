using System;
using System.Collections.Generic;
using Xunit;

namespace WordleGame
{
    class Program
    {

        // In this code, the Word class has been created to represent a word in the game,
        // and the WordQueue class extends the Queue<Word> class to create a queue of words for the game.
        // The WordFactory class provides a factory method for creating Word objects,
        // so that future modifications to the Word class can be easily handled
        // .A simple unit test for the Word class has also been included using a testing framework, such as XUnit.
        static void Main(string[] args)
        {
            WordQueue wordQueue = new WordQueue();
            wordQueue.Enqueue(WordFactory.CreateWord("apple"));
            wordQueue.Enqueue(WordFactory.CreateWord("banana"));
            wordQueue.Enqueue(WordFactory.CreateWord("cherry"));

            while (wordQueue.Count > 0)
            {
                Word wordToGuess = wordQueue.Dequeue();
                char[] currentWord = new char[] { '_', '_', '_', '_', '_' };
                int numberOfGuesses = 0;

                while (!new string(currentWord).Equals(wordToGuess.Value))
                {
                    Console.WriteLine("Current word: " + new string(currentWord));
                    Console.WriteLine("Enter a letter: ");
                    char guess = Console.ReadLine()[0];
                    numberOfGuesses++;

                    for (int i = 0; i < wordToGuess.Value.Length; i++)
                    {
                        if (wordToGuess.Value[i] == guess)
                        {
                            currentWord[i] = guess;
                        }
                    }
                }

                Console.WriteLine("You won! The word was " + wordToGuess.Value + " and it took you " + numberOfGuesses + " guesses.");
                Console.WriteLine("Enter 'yes' to play again, or 'no' to quit: ");
                string playAgain = Console.ReadLine();
                if (playAgain.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    wordQueue.Enqueue(wordToGuess);
                }
                else if (playAgain.Equals("no", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Enter a new word to add to the queue: ");
                    string newWord = Console.ReadLine();
                    if (ValidateWord(newWord))
                    {
                        wordQueue.Enqueue(WordFactory.CreateWord(newWord));
                    }
                    else
                    {
                        Console.WriteLine("Invalid word. Only alphabetic characters are allowed.");
                    }
                }
            }

            Console.WriteLine("No more words in the queue. Goodbye!");
            Console.ReadLine();
        }

        static bool ValidateWord(string word)
        {
            foreach (char c in word)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
    }

    class WordQueue : Queue<Word>
    {
    }

    class Word
    {
        public string Value { get; set; }

        public Word(string value)
        {
            Value = value;
        }
    }

    class WordFactory
    {
        public static Word CreateWord(string value)
        {
            return new Word(value);
        }
    }
    class WordTests
    {
        [Fact]
        public void WordValueIsCorrect()
        {
            Word word = new Word("test");
            Assert.Equal("test", word.Value);
        }
    }
}

