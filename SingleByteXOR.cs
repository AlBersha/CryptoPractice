using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.Int32;

namespace Crypto
{
    public class SingleByteXOR
    {
        public string Input { get; set; }
        public List<int> Hexs { get; set; }
        public List<char> ASCII { get; set; }

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
    }
}