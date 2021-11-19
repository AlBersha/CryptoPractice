using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            #region singlebyte XOR
            // var XORinput = "7958401743454e1756174552475256435e59501a5c524e176f786517545e475f5245191772195019175e4317445f58425b531743565c521756174443455e595017d5b7ab5f525b5b58174058455b53d5b7aa175659531b17505e41525917435f52175c524e175e4417d5b7ab5c524ed5b7aa1b174f584517435f5217515e454443175b524343524517d5b7ab5fd5b7aa17405e435f17d5b7ab5cd5b7aa1b17435f5259174f584517d5b7ab52d5b7aa17405e435f17d5b7ab52d5b7aa1b17435f525917d5b7ab5bd5b7aa17405e435f17d5b7ab4ed5b7aa1b1756595317435f5259174f58451759524f4317545f564517d5b7ab5bd5b7aa17405e435f17d5b7ab5cd5b7aa175650565e591b17435f525917d5b7ab58d5b7aa17405e435f17d5b7ab52d5b7aa1756595317445817585919176e5842175a564e17424452175659175e5953524f1758511754585e59545e53525954521b177f565a5a5e595017535e4443565954521b177c56445e445c5e17524f565a5e5956435e58591b17444356435e44435e54565b17435244434417584517405f564352415245175a52435f5853174e5842175152525b174058425b5317445f584017435f52175552444317455244425b4319";
            //
            // var XOR = new SingleByteXOR(XORinput);
            // var attempts = XOR.ImprovedBruteForce();
            //
            // foreach (var attempt in attempts)
            // {
            //     Console.WriteLine(attempt.Key+ "\n" + attempt.Value + "\n");
            // }
            #endregion

            #region repeating-key XOR
            // var repeating-keyXORinput = "G0IFOFVMLRAPI1QJbEQDbFEYOFEPJxAfI10JbEMFIUAAKRAfOVIfOFkYOUQFI15ML1kcJFUeYhA4IxAeKVQZL1VMOFgJbFMDIUAAKUgFOElMI1ZMOFgFPxADIlVMO1VMO1kAIBAZP1VMI14ANRAZPEAJPlMNP1VMIFUYOFUePxxMP19MOFgJbFsJNUMcLVMJbFkfbF8CIElMfgZNbGQDbFcJOBAYJFkfbF8CKRAeJVcEOBANOUQDIVEYJVMNIFwVbEkDORAbJVwAbEAeI1INLlwVbF4JKVRMOF9MOUMJbEMDIVVMP18eOBADKhALKV4JOFkPbFEAK18eJUQEIRBEO1gFL1hMO18eJ1UIbEQEKRAOKUMYbFwNP0RMNVUNPhlAbEMFIUUALUQJKBANIl4JLVwFIldMI0JMK0INKFkJIkRMKFUfL1UCOB5MH1UeJV8ZP1wVYBAbPlkYKRAFOBAeJVcEOBACI0dAbEkDORAbJVwAbF4JKVRMJURMOF9MKFUPJUAEKUJMOFgJbF4JNERMI14JbFEfbEcJIFxCbHIJLUJMJV5MIVkCKBxMOFgJPlVLPxACIxAfPFEPKUNCbDoEOEQcPwpDY1QDL0NCK18DK1wJYlMDIR8II1MZIVUCOB8IYwEkFQcoIB1ZJUQ1CAMvE1cHOVUuOkYuCkA4eHMJL3c8JWJffHIfDWIAGEA9Y1UIJURTOUMccUMELUIFIlc=";
            // repeating-keyXORinput = Encoding.UTF8.GetString(Convert.FromBase64String(repeating-keyXORinput));
            // var hack = new RepeatedKeyXOR(repeating-keyXORinput);
            // var stat = hack.IndexOfCoincidence();
            //
            // var smth = hack.FrequencyAnalysis(3);
            // foreach (var elem in smth)
            // {
            //     Console.Out.WriteLine("---------------------------------------------------------------------------------");
            //     foreach (var t in elem)
            //     {
            //         Console.Out.WriteLine($"Key - {t.Key}");
            //         foreach (var item in t.Value)
            //         {
            //             Console.Out.Write($"{item}");
            //         }
            //         Console.Out.WriteLine("");
            //     }
            // }

            // Console.Out.WriteLine(hack.RepeatedKeyXORFunc("L0l"));
            #endregion

            #region substituion

            var substituioninput =
                "EFFPQLEKVTVPCPYFLMVHQLUEWCNVWFYGHYTCETHQEKLPVMSAKSPVPAPVYWMVHQLUSPQLYWLASLFVWPQLMVHQLUPLRPSQLULQESPBLWPCSVRVWFLHLWFLWPUEWFYOTCMQYSLWOYWYETHQEKLPVMSAKSPVPAPVYWHEPPLUWSGYULEMQTLPPLUGUYOLWDTVSQ" +
                "ETHQEKLPVPVSMTLEUPQEPCYAMEWWYTYWDLUULTCYWPQLSEOLSVOHTLUYAPVWLYGDALSSVWDPQLNLCKCLRQEASPVILSLEUMQBQVMQCYAHUYKEKTCASLFPYFLMVHQLUPQLHULIVYASHEUEDUEHQBVTTPQLVWFLRYGMYVWMVFLWMLSPVTTBYUNESESADDLSPV" +
                "YWCYAMEWPUCPYFVIVFLPQLOLSSEDLVWHEUPSKCPQLWAOKLUYGMQEUEMPLUSVWENLCEWFEHHTCGULXALWMCEWETCSVSPYLEMQYGPQLOMEWCYAGVWFEBECPYASLQVDQLUYUFLUGULXALWMCSPEPVSPVMSBVPQPQVSPCHLYGMVHQLUPQLWLRPOEDVMETBYUFB" +
                "VTTPENLPYPQLWLRPTEKLWZYCKVPTCSTESQPBYMEHVPETCMEHVPETZMEHVPETKTMEHVPETCMEHVPETT";

            const string filePath = "C:\\Users\\obers\\pp\\KPI\\7 term\\#crypto\\pract\\Crypto\\Genetic Algo\\Threegrams.txt";
            var ngram = ReadNgramFromFile(filePath);
            var geneAlgo = new GeneticAlgorithm(ngram, 700);

            geneAlgo.GeneAlgorithm(substituioninput);
            #endregion


        }

        private static Dictionary<string, double> ReadNgramFromFile(string filePath)
        {
            var ngrams = new Dictionary<string, double>();
            if (!File.Exists(filePath)) return ngrams;
            using var sr = new StreamReader(filePath);
            while (sr.Peek() >= 0)
            {
                var line = sr.ReadLine();
                var ngram = line?[..line.IndexOf(" ", StringComparison.Ordinal)];
                var freq = double
                .Parse(line[(line.IndexOf(" ")+1)..]);
                    
                ngrams.Add(ngram, freq);
            }

            return ngrams;
        }
    }
}