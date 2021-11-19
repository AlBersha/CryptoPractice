using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Crypto
{
    public class Gene: IComparable<Gene> 
    {
        public List<char> gene { get; set; }
        public double fitness { get; set; }

        public Gene()
        {
            gene = new List<char>();
            for (var i = 0; i < 26; i++)
            {
                gene.Add((char)i);
            }
        }

        public Gene(List<char> gene)
        {
            this.gene = gene;
        }

        public string Decrypt(string line)
        {
            return line.Aggregate("", (current, t) => current + gene[t - 'A']);
        }

        public int CompareTo(Gene other)
        {
            if (ReferenceEquals(this, other)) return 0;
            return ReferenceEquals(null, other) ? 1 : fitness.CompareTo(other.fitness);
        }
    }
}