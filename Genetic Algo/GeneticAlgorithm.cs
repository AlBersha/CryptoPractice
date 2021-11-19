using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;

namespace Crypto
{
    public class GeneticAlgorithm
    {
        private List<Gene> CurrentPopulation { get; set; }
        private Dictionary<string, double> Trigrams { get; }
        private readonly Random random;
        private int PopulationSize { get; }

        public GeneticAlgorithm(Dictionary<string, double> trigrams, int populationSize )
        {
            Trigrams = trigrams;
            PopulationSize = populationSize;
            CurrentPopulation = new List<Gene>();
            random = new Random();
        }

        private void CreatePopulation(string input)
        {
            CurrentPopulation.Clear();
            for (var i = 0; i < PopulationSize; i++)
            {
                var gene = new Gene(CreateRowGene());
                CurrentPopulation.Add(gene);
            }
            
            Evaluate(input);
        }

        private List<char> CreateRowGene()
        {
            var gene = new List<char>();
            for (var i = 65; i < 91; i++)
            {
                gene.Add((char)i);
            }

            return gene.OrderBy(_ => random.Next()).ToList();
        }
        
        public void GeneAlgorithm(string input){
            CreatePopulation(input);
            
            var fits = new List<double> { 1.0 };
            var generations = 0;
            while (true)
            {
                NextGeneration(input);
                generations++;
                Console.Out.WriteLine(CurrentPopulation[0].fitness);

                if (CurrentPopulation[0].fitness < fits[0])
                {
                    fits.Clear();
                }

                fits.Add(CurrentPopulation[0].fitness);
                if (fits.Count <= 80) continue;
                break;
            }

            Console.Out.WriteLine("");
            foreach (var item in CurrentPopulation[0].gene)
            {
                Console.Out.Write(item);
            }

            Console.Out.WriteLine($"\nGenerations amount = {generations}\n");
            
            Console.Out.WriteLine(CurrentPopulation[0].Decrypt(input));
        }

        private void NextGeneration(string input)
        {
            var nextGeneration = new List<Gene>();
            for (var i = 0; i < PopulationSize/7; i++) {
                nextGeneration.Add(CurrentPopulation[i]);
            }

            var secondParent = CurrentPopulation[Selection()];
            for (var i = PopulationSize/7; i < PopulationSize; i+=2 ){
                var firstParent = CurrentPopulation[Selection()];

                Crossover(firstParent, secondParent, nextGeneration);
            }
            CurrentPopulation.Clear();
            CurrentPopulation = nextGeneration;
            
            Evaluate(input);
        }

        private void Crossover(Gene firstParent, Gene secondParent, ICollection<Gene> genes)
        {
            var end = random.Next(firstParent.gene.Count + 1);
            var start = random.Next(end);

            var childFirst = new Gene();
            var childSecond = new Gene();

            for (var i = start; i < end; i++) {
                var first = firstParent.gene[i];
                var second = secondParent.gene[i];

                childFirst.gene[i] = second;
                childSecond.gene[i] = first;

            }

            for (var i = 0; i < firstParent.gene.Count; i++) {
                if(i==start){
                    i+=end-start;
                    if(i == firstParent.gene.Count)
                        continue;
                }

                var ind = i;
                while(childFirst.gene.Contains(firstParent.gene[ind])){
                    ind = childFirst.gene.IndexOf(firstParent.gene[ind]);
                }
                childFirst.gene[i] = firstParent.gene[ind];

                ind = i;
                while(childSecond.gene.Contains(secondParent.gene[ind])){
                    ind = childSecond.gene.IndexOf(secondParent.gene[ind]);
                }
                childSecond.gene[i] = secondParent.gene[ind];

            }

            genes.Add(childFirst);
            genes.Add(childSecond);
        }

        private void Evaluate(string input)
        {
            for (var i = 0; i < PopulationSize; i++) {
                var gene = CurrentPopulation[i];
                var decrypt = gene.Decrypt(input);
                var decryptMap = GetNgramsFreq(3,decrypt);
                CurrentPopulation[i].fitness = Fitness(decryptMap, Trigrams);
            }
            
            CurrentPopulation.Sort();
        }

        private Dictionary<string, double> GetNgramsFreq(int n, string text)
        {
            var ngrams = new Dictionary<string, double>();
            var count = 0;
            for (var i = 0; i < text.Length - n + 1; i++) {
                var ngram = text.Substring(i, n);
                if (ngrams.ContainsKey(ngram))
                {
                    ngrams[ngram] += 1;
                }
                else
                {
                    ngrams.Add(ngram, 1);
                }
                count++;
            }

            foreach (var pair in ngrams)
            {
                ngrams[pair.Key] /= count;
            }

            return ngrams;
        }

        private int Selection()
        {
            return random.Next(0, CurrentPopulation.Count / 3);

            // var totalFitness = CurrentPopulation.Sum(t => t.fitness);
            // var rand = random.Next((int)totalFitness);
            // var partialSum = .0;
            // for (var i = 0; i < CurrentPopulation.Count; i++) {
            //     partialSum+= CurrentPopulation[i].fitness;
            //     if(partialSum >= rand)
            //         return i;
            // }
            // return -1;
        }
        
        private double Fitness( Dictionary<string, double> textTrigrams, Dictionary<string, double> trigrams ){
            var score = .0;
            foreach (var (key, freq) in textTrigrams)
            {
                var freqOrig = trigrams.ContainsKey(key) ? trigrams[key] : .0;
                score += freq - freqOrig;

            }
            return score;
        }

    }
}