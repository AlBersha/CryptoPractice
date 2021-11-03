using System.Collections.Generic;
using System.Linq;

namespace Crypto
{
    public class Substitution
    {
        private string Input { get; }

        public Substitution(string input)
        {
            Input = input;
        }

        public Dictionary<char, int> CountFrequency()
        {
            var alphabet = new List<char>(Enumerable.Range(65, 26).ToArray().Select(item => (char) item).ToList());
            var frequency = new Dictionary<char, int>();
            foreach (var letter in alphabet)
            {
                var num = Input.Where(c => c == letter).ToList().Count;
                frequency.Add(letter, num);
            }

            return SortDictionaryByValue(frequency);
        }

        private Dictionary<char, int> SortDictionaryByValue(Dictionary<char, int> dictionary)
        {
            return dictionary.OrderByDescending(c => c.Value).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}