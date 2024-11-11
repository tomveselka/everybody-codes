using System.Diagnostics.Tracing;

namespace EverybodyCodes2024;

public class Task2
{
    FileReader reader = new FileReader();
    public void Part2 (string fileName)
    {
        var input = reader.ReadFile(fileName);
        var wordsArray = input[0].Replace("WORDS:", "").Split(",");
        var words = new HashSet<string>();
        foreach (var word in wordsArray)
        {
            words.Add(word);
            words.Add(ReverseString(word));
        }

        var inscriptions = new List<string>();
        for (int i = 2; i < input.Length; i++)
        {
            inscriptions.Add(input[i]);
        }
        var finalCount = 0;
        var row = 0;
        foreach (var inscription in inscriptions)
        {
            var positions = new HashSet<int>();
            foreach (var word in words)
            {
                if(inscription.IndexOf(word, 0) != -1)
                {
                    for (int i = 0; i < inscription.Length; i++)
                    {
                        var pos = inscription.IndexOf(word, i);
                        if (pos != -1)
                        {
                            for (int j = 0; j<word.Length; j++)
                            {
                                positions.Add(pos + j);
                            }
                        }
                    }
                }                
            }
            finalCount += positions.Count;
            row++;
            Console.WriteLine("Row {0}: {1} runic symbols", row, positions.Count);
        }
        Console.WriteLine("Final count is: {0} runic symbols", finalCount);
    }

    private string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
