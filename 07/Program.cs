using System.Numerics;
using Common;
using Utils;

namespace Day_07
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static BigInteger _resultTwo = 0;

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            var input = await FileUtils.ReadTextFileAsync(filePath);
            Logger.Log($"[Program.Run]\n{input}");

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount(input);
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount(input);
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void PartOneCount(string input)
        {
            var lines = input.Split('\n');
            Logger.Log($"{lines[0]}");
            for (var l = 1; l < lines.Length; l++)
            {
                var line = lines[l];
                var ca = line.ToCharArray();
                for (var c = 0; c < line.Length; c++)
                {
                    if (lines[l - 1][c] == 'S')
                    {
                        ca[c] = '|';
                    }
                    if (lines[l][c] == '^' && lines[l - 1][c] == '|')
                    {
                        if (c > 0)
                            ca[c - 1] = '|';
                        if (c < line.Length)
                            ca[c + 1] = '|';
                        _result++;
                    }
                    if (lines[l][c] == '.' && lines[l - 1][c] == '|')
                    {
                        ca[c] = '|';
                    }
                }
                lines[l] = new string(ca);
                Logger.Log($"{lines[l]}");
            }
        }

        private static void PartTwoCount(string input)
        {
            
        }
    }
}
