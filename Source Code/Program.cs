using System;
using System.Collections.Generic;
using System.Linq;

namespace HangmanGuessBot
{
    class Program
    {
        static List<char> Characters = "abcdefghijklmnopqrstuvwxyzƒ".ToCharArray().ToList<char>();
        static int lives = 5;

        static void Main(string[] args)
        {
            #region Setup
            Console.Clear();
            Console.WriteLine("How many lives do i have?");
            lives = int.Parse(Console.ReadLine());
            Console.Clear();

            List<string> NoTrimlines = System.IO.File.ReadAllLines(@"C:\Users\rasmu\Desktop\Words.txt").ToList<string>();
            Console.WriteLine("How many letters in your word?");
            int LetterCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Before there were " + NoTrimlines.Count + " words");

            List<string> lines = new List<string>();

            foreach (string line in NoTrimlines)
            {
                if(line.Length == LetterCount)
                {
                    lines.Add(line);
                }
            }
            Console.WriteLine("Now there are " + lines.Count + " words");
            #endregion

            GuessLetter(lines);
        }

        
        static void GuessLetter(List<string> lines)
        {
            #region bestChar
            int i = 100000;
            Char bestChar = 'a';
            foreach (char char_m in Characters)
            {
                int j = 0;
                foreach (string line in lines)
                {
                    if (!line.Contains(char_m))
                    {
                        j++;
                    }
                }
                if(j < i)
                {
                    i = j;
                    bestChar = char_m;
                }
            }
            #endregion

            Console.Clear();
            Console.WriteLine("Does your word contain the letter " + bestChar + "?");
            Console.WriteLine("Yes/No");

            string answer = Console.ReadLine();

            Console.Clear();

            if(answer == "words")
            {
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Does your word contain the letter " + bestChar + "?");
                Console.WriteLine("Yes/No");

                answer = Console.ReadLine();
                Console.Clear();
            }

            if (answer == "Yes" || answer == "yes")
            {
                Characters.Remove(bestChar);

                Console.WriteLine("Which character(s) is it? Ex: 2 5 6");
                List<string> CharPlace = Console.ReadLine().Split(' ').ToList<string>();

                List<string> NewLines = new List<string>();
                foreach (string line in lines)
                {
                    bool good = true;
                    foreach (string CharPlacements in CharPlace)
                    {
                        if(line[int.Parse(CharPlacements) - 1] != bestChar)
                        {
                            good = false;
                        }
                    }
                    if (good)
                    {
                        NewLines.Add(line);
                    }
                }
                lines = NewLines;
                if(lines.Count == 1)
                {
                    Console.WriteLine("The word is " + lines[0]);
                    Console.ReadKey();
                    System.Environment.Exit(1);
                }

            } else if (answer == "No" || answer == "no")
            {
                Characters.Remove(bestChar);
                lives--;
                if(lives == 0)
                {
                    Console.Clear();
                    Console.WriteLine("i lost :(");
                    Console.ReadKey();
                    System.Environment.Exit(1);
                } else
                {
                    Console.WriteLine("i have " + lives + " lives left...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } else
            {
                System.Environment.Exit(1);
            }
            if(lines.Count <= 0)
            {
                Console.WriteLine("There are no such words...");
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            GuessLetter(lines);
        }
    }
}