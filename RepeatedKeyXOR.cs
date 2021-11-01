using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Crypto
{
    public class RepeatedKeyXOR
    {
        private string Input { get; set; }

        public RepeatedKeyXOR(string input)
        {
            Input = input;
        }

        public List<int> IndexOfCoincidence()
        {
            var shift = 1;
            var coincidences = new List<int>();

            foreach (var amountOfCoincidence 
                in Input.Select(t1 => Input.Where((t, j) => t == Input[j + shift < Input.Length ? j + shift : j + shift - Input.Length]).Count()))
            {
                coincidences.Add(amountOfCoincidence);
                shift++;
            }

            return coincidences;
        }

        public List<Dictionary<int, List<char>>> FrequencyAnalysis(int keyLength)
        {
            var substrings = new List<string>();
            for (var i = 0; i < keyLength; i++)
            {
                var substring = "";
                for (var j = i; j < Input.Length; j+= keyLength)
                {
                    substring += Input[j];
                }
                substrings.Add(substring);
            }

            var results = new List<Dictionary<int, List<char>>>();
            var ASCII = new List<char>(Enumerable.Range(0, 255).ToArray().Select(item => (char) item).ToList());
            var max = 0;

            foreach (var sub in substrings)
            {
                var outs = new Dictionary<int, List<char>>();
                foreach (var symb in ASCII)
                {
                    // var xoredstring = sub.Aggregate("", (current, t) => current + (t ^ symb));
                    var xoredstring = sub.Select(item => (char) (item ^ symb)).ToList();
                    var num = CountLetters(xoredstring);
                    if (max <= num)
                    {
                        if (max < num)
                        {
                            outs.Clear();
                            max = num;
                        }
                        outs.Add(symb, xoredstring);
                    }
                }

                max = 0;
                results.Add(new Dictionary<int, List<char>>(outs));
                outs.Clear();
            }

            return results;
        }

        private int CountLetters(List<char> xoredstring)
        {
            return xoredstring.Count(symb => symb > 64 && symb < 123);
        }

        public string RepeatedKeyXORFunc(string key)
        {
            var xoredstring = "";
            for (var i = 0; i < Input.Length; i++)
            {
                xoredstring += (char)(Input[i] ^ key[i % key.Length]);
            }

            return xoredstring;
        }
    }
}