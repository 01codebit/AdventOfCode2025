using System.Numerics;
using Common;
using Models;
using Utils;

namespace Day_06
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static BigInteger _resultTwo = 0;
        private static List<Problem> _problems = [];

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            var input = await FileUtils.ReadTextFileAsync(filePath);
            Logger.Log($"[Program.Run]\n{input}");
            ParseInput(input);
            foreach (var p in _problems)
                Logger.Log($"problem: {p}");

            Logger.LogLine($"Read {_problems.Count} problems with {_problems[0].Numbers.Count} numbers");


            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void ParseInput(string input)
        {
            var lines = input.Split('\r');

            foreach (var line in lines)
            {
                var parts = line?.Split(' ');
                if (parts != null)
                {
                    Logger.Log($"parts.Length: {parts.Length} - [{string.Join(',', parts)}]");

                    List<string> cparts = [];
                    foreach (var p in parts)
                    {
                        if (!string.IsNullOrEmpty(p) && p != "\n")
                            cparts.Add(p);
                    }

                    for (var c = 0; c < cparts.Count; c++)
                    {
                        if (_problems.Count < c + 1)
                        {
                            _problems.Add(new Problem());
                        }

                        var value = cparts[c].Trim();
                        Logger.Log($"c: {c} value: '{value}'");

                        if (long.TryParse(value, out long result))
                        {
                            _problems[c].Numbers.Add(result);
                        }
                        else
                        {
                            _problems[c].Operand = value[0];
                        }
                    }
                }
            }
        }

        private static void PartOneCount()
        {
            foreach (var p in _problems)
            {
                _result += p.Result();
            }
        }

        private static void PartTwoCount() { }
    }
}
