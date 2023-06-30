using System;
using System.Linq;
using System.Collections.Generic;
namespace TextToolKit
{
    public class Analyze
    {
        public Analyze()
        {
        }

        public void CreateAnalysis(string text)
        {
            //Character Data

            //Char Counts
            int letterCount = text.Where(x => char.IsLetter(x)).Count();
            int numberCount = text.Where(x => char.IsNumber(x)).Count();
            int spaceCount = text.Where(x => x == ' ').Count();
            int otherCount = text.Where(x => !char.IsLetter(x) && !char.IsNumber(x) && x != ' ').Count();
            int totalCount = text.Length;

            //Char split, group, count
            var splitChar = text.ToLower().Select(x => x.ToString());
            var statsChar = splitChar.GroupBy(x => x).Select(x => x.Key).Select(x => new { Character = x, CharCount = splitChar.Where(y => y == x).Count() });

            //Get the Most Used Character
            int maxCount = statsChar.Max(x => x.CharCount);
            string mostChar = "";
            if (statsChar.All(x => x.CharCount == maxCount))
            {
                mostChar = "All characters occur the same amount of times.";
            }
            else
            {
                var maxCharGroup = statsChar.Where(x => x.CharCount == maxCount).Select(x => x.Character).OrderBy(x => x);
                mostChar = maxCharGroup.Count() > 1 ? string.Join(" ", maxCharGroup).Replace("  ", "Space ") : maxCharGroup.First().Replace(" ", "Space");
                if (mostChar.Contains("Space"))
                {
                    if (mostChar == "Space")
                    {
                        int nextMaxCount = statsChar.Where(x => x.Character != " ").Max(x => x.CharCount);
                        var nextMaxCharGroup = statsChar.Where(x => x.CharCount == nextMaxCount).Select(x => x.Character).OrderBy(x => x);
                        mostChar += " followed by ";
                        mostChar += nextMaxCharGroup.Count() > 1 ? string.Join(" ", nextMaxCharGroup) : nextMaxCharGroup.First();
                    }
                    else
                    {
                        mostChar = mostChar.Replace("Space", "").Trim();
                        mostChar += " Space";
                    }
                }
            }
            //Get the Least Used Character
            string leastChar = "";
            if (mostChar == "All characters occur the same amount of times.")
            {
                leastChar = "All characters occur the same amount of times.";
            }
            else
            {
                int minCount = statsChar.Min(x => x.CharCount);
                var minCharGroup = statsChar.Where(x => x.CharCount == minCount).Select(x => x.Character).OrderBy(x => x);
                leastChar = minCharGroup.Count() > 1 ? string.Join(" ", minCharGroup).Replace("  ", "Space ") : minCharGroup.First().Replace(" ", "Space");
                if (leastChar.Contains("Space"))
                {
                    if (leastChar == "Space")
                    {
                        int nextMinCount = statsChar.Where(x => x.Character != " ").Min(x => x.CharCount);
                        var nextMinCharGroup = statsChar.Where(x => x.CharCount == nextMinCount).Select(x => x.Character).OrderBy(x => x);
                        leastChar += " followed by ";
                        leastChar += nextMinCharGroup.Count() > 1 ? string.Join(" ", nextMinCharGroup) : nextMinCharGroup.First();
                    }
                    else
                    {
                        leastChar = mostChar.Replace("Space", "").Trim();
                        leastChar += " Space";
                    }
                }
            }


            //Word Data

            List<string> splitWords = new List<string>();
            if (text.Contains(' '))
            {
                string replace = text.Replace('.', ' ').Replace('!', ' ').Replace('?', ' ').Replace(',', ' ').Replace(';', ' ').Replace(':', ' ');
                if (replace.Any(x => x != ' '))
                {
                    splitWords = replace.ToLower().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
                }
            }

            int wordCount = splitWords.Count;

            var statsWord = wordCount > 0 ? splitWords.GroupBy(x => x).Select(x => x.Key).Select(x => new { Word = x, WordCount = splitWords.Where(y => y == x).Count() }) : null;

            string mostWord = "";
            string leastWord = "";

            if (wordCount == 0)
            {
                mostWord = "Entry contains no words.";
                leastWord = "Entry contains no words.";
            }
            else
            {
                int maxWordCount = statsWord.Max(x => x.WordCount);
                if (statsWord.All(x => x.WordCount == maxWordCount))
                {
                    mostWord = "All words occur the same amount of times.";
                }
                else
                {
                    var maxWordGroup = statsWord.Where(x => x.WordCount == maxWordCount).Select(x => x.Word).OrderBy(x => x);
                    mostWord = maxWordGroup.Count() > 1 ? string.Join(", ", maxWordGroup) : maxWordGroup.First();
                }

                if (mostWord == "All words occur the same amount of times.")
                {
                    leastWord = "All words occur the same amount of times.";
                }
                else
                {
                    int minWordCount = statsWord.Min(x => x.WordCount);
                    var minWordGroup = statsWord.Where(x => x.WordCount == minWordCount).Select(x => x.Word).OrderBy(x => x);
                    leastWord = minWordGroup.Count() > 1 ? string.Join(", ", minWordGroup) : minWordGroup.First();
                }
            }

            //User View

            Console.Clear();
            Console.WriteLine("Entered Text:");
            Console.WriteLine("_____________");
            Console.WriteLine(text);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("________________________________________________________________________________________________");
            Console.WriteLine("* If space is the Most Used Character, it will be followed by the next most used character(s).");
            Console.WriteLine("* If space is the Least Used Character, it will be followed by the next least used character(s).");
            Console.WriteLine("* Letter casing is ignored in this analysis. All will be counted as lower case.");
            Console.WriteLine("* Words are counted as characters before a space or ending punctuation ( .!?,;: ).");
            Console.WriteLine("________________________________________________________________________________________________");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Single Character Data");
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Letter Count", letterCount));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Number Count", numberCount));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Space Count", spaceCount));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Other Character Count", otherCount));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Total Character Count", totalCount));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Most Used Character(s)", mostChar));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Least Used Character(s)", leastChar));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Word Data");
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Word Count", wordCount));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Most Used Word(s)", mostWord));
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Least Used Word(s)", leastWord));
            Console.WriteLine("_________________________________________________________________________________");

            string charsDisplay = "abcdefghijklmnopqrstuvwxyz0123456789`~!@#$%^&*()-_=+[]{}\\|;:'\",.<>/?";

            //Next, work on indivdual char display and count
            //Then, work on word display and count
        }
    }
}
