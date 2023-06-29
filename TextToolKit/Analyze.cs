using System;
using System.Linq;
namespace TextToolKit
{
    public class Analyze
    {
        public Analyze()
        {
        }

        public void CreateAnalysis(string text)
        {
            int letterCount = text.Where(x => char.IsLetter(x)).Count();
            int numberCount = text.Where(x => char.IsNumber(x)).Count();
            int spaceCount = text.Where(x => x == ' ').Count();
            int otherCount = text.Where(x => !char.IsLetter(x) && !char.IsNumber(x) && x != ' ').Count();
            int totalCount = text.Length;

            int wordCount = 0;
            if (text.Contains(' ') && text.Any(x => x != ' '))
            {
                wordCount = text.Split(' ').Where(x => !x.Contains(' ')).Count();
            }

            var statsChar = text.ToLower().Select(x => new { Character = x, CharCount = text.Where(y => y == x).Count() });

            string mostChar = statsChar.OrderBy(x => x.CharCount).Select(x => x.Character).Last().ToString();
            if (mostChar == " " && statsChar.Count() > 1)
            {
                var second = statsChar.OrderBy(x => x.CharCount).Select(x => x.Character).ToArray();
                string secMostChar = second[second.Length - 2].ToString();
                mostChar = $"Space. followed by {secMostChar}";
            }
            string leastChar = statsChar.OrderBy(x => x.CharCount).Select(x => x.Character).First().ToString();
            if (leastChar == " " && statsChar.Count() > 1)
            {
                var second = statsChar.OrderBy(x => x.CharCount).Select(x => x.Character).ToArray();
                string secLeastChar = second[1].ToString();
                mostChar = $"Space. followed by {secLeastChar}";
            }

            var splitWord = wordCount > 0 ? text.ToLower().Split(' ').Where(x => !x.Contains(' ')) : null;
            var statsWord = splitWord != null && splitWord.Count() > 0 ? splitWord.Select(x => new { Word = x, WordCount = splitWord.Where(y => y == x).Count() }) : null;

            string mostWord = statsWord != null && statsWord.Count() > 0 ? statsWord.OrderBy(x => x.WordCount).Select(x => x.Word).Last() : "-------------------";
            string leastWord = statsWord != null && statsWord.Count() > 0 ? statsWord.OrderBy(x => x.WordCount).Select(x => x.Word).First() : "-------------------";


            Console.Clear();
            Console.WriteLine(text);

            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Letter Count", letterCount));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Number Count", numberCount));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Space Count", spaceCount));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Other Character Count", otherCount));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Total Character Count", totalCount));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Word Count", wordCount));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Most Used Character", mostChar));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Least Used Character", leastChar));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Most Used Word", mostWord));
            Console.WriteLine("_______________________________________________");
            Console.WriteLine(String.Format("{0,-25} |    {1, -21}", "Least Used Word", leastWord));
            Console.WriteLine("_______________________________________________");



            //Make a note to user that all case is ignored
            //Make note to user that words are based on spaces and ending punctuations in entry text
        }
    }
}
