using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.Int32;

namespace Crypto
{
    public class SingleByteXOR
    {
        private string Input { get; set; }
        private List<int> Hexs { get; set; }
        private List<char> ASCII { get; set; }

        public SingleByteXOR(string input)
        {
            Input = input;
            Hexs = new List<int>();
            
            for (var i = 0; i < input.Length / 2; i++)
            {
                Hexs.Add(Parse(Input[..2], NumberStyles.HexNumber));
                Input = Input[2..];
            }

            ASCII = new List<char>(Enumerable.Range(0, 255).ToArray().Select(item => (char) item).ToList());
        }

        public Dictionary<string, string> BruteForce()
        {
            var outs = new Dictionary<string, string>();
            
            foreach (var symb in ASCII)
            {
                var output = Hexs.Aggregate("", (current, hex) => current + char.ToString((char) (hex ^ symb)));
                outs.Add(symb.ToString(), output);
            }

            return outs;
        }

        public Dictionary<string, string> ImprovedBruteForce()
        {
            var outs = new Dictionary<string, string>();
            var amount = 0;
            foreach (var symb in ASCII)
            {
                var output = Hexs.Aggregate("", (current, hex) => current + char.ToString((char) (hex ^ symb)));
                var n = output.Count(c => c > 64 && c < 123);
                if (amount <= n)
                {
                    if (amount < n)
                    {
                        outs.Clear();
                        amount = n;
                    }
                    outs.Add(symb.ToString(), output);
                }
            }
            
            return outs;
        }
    }
}