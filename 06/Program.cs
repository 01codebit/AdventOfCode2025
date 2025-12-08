using System.Numerics;
using System.Text;
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

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount(input);
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount(input);
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

                        var value = cparts[c];
                        Logger.Log($"c: {c} value: '{value}'");

                        if (value.Contains('+') || value.Contains('*'))
                        {
                            _problems[c].Operand = value[0];
                        }
                        else
                        {
                            _problems[c].Numbers.Add(value);
                        }
                    }
                }
            }
        }

        private static string Reverse(string input)
        {
            var sb = new StringBuilder();
            for (var i = input.Length - 1; i >= 0; i--)
            {
                sb.Append(input[i]);
            }
            Logger.Log($"[Reverse] input: '{input}' return '{sb.ToString()}'");
            return sb.ToString();
        }

        private static void PartOneCount(string input)
        {
            ParseInput(input);

            foreach (var p in _problems)
                Logger.Log($"problem: {p}");

            Logger.LogLine(
                $"Read {_problems.Count} problems with {_problems[0].Numbers.Count} numbers"
            );

            foreach (var p in _problems)
            {
                _result += p.Result();
            }
        }

        private static void PartTwoCount(string input)
        {
            ParseInputTwo(input);

            foreach (var p in _problems)
                Logger.Log($"problem: {p}");

            Logger.LogLine(
                $"Read {_problems.Count} problems with {_problems[0].Numbers.Count} numbers"
            );

            foreach (var p in _problems)
            {
                _resultTwo += p.Result();
            }
        }

        private static void ParseInputTwo(string input)
        {
            var outputSb = new StringBuilder();
            input = input.Replace('\r', '\n');
            var lines = input.Split('\n');

            for (var i = lines[0].Length - 1; i >= 0; i--)
            {
                var nl = new StringBuilder();
                foreach (var line in lines)
                {
                    if (line.Length != 0)
                    {
                        nl.Append(line[i]);
                        if (line[i] == '+' || line[i] == '*')
                            nl.Append('\n');
                    }
                }

                outputSb.Append(nl);
            }

            var outLines = outputSb.ToString().Split('\n');
            _problems.Clear();
            foreach (var l in outLines)
            {
                if (l != "")
                    _problems.Add(ParseFromString(l));
            }
        }

        private static Problem ParseFromString(string input)
        {
            var result = new Problem();

            var operand = input[^1];
            result.Operand = operand;

            var values = input[..^1];

            var tokens = values.Split(' ');
            foreach (var token in tokens)
            {
                if (token == "")
                    continue;

                result.Numbers.Add(token.Trim());
            }

            return result;
        }
    }
}
