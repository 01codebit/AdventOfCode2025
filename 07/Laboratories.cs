using System.Numerics;
using Common;
using Utils;

namespace Day_07
{
    public class Laboratories : IRunnablePart
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
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void PartOneCount(string input)
        {
            input = input.Replace("\r", "");
            var lines = input.Split('\n');

            long timelines = 0;

            long[] values = new long[lines[0].Length];
            long[] prevValues = new long[lines[0].Length];
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = 0;
                prevValues[i] = 0;
            }

            for (var l = 1; l < lines.Length; l++)
            {
                var line = lines[l];
                var ca = line.ToCharArray();

                for (var c = 0; c < line.Length; c++)
                {
                    if (lines[l - 1][c] == 'S')
                    {
                        ca[c] = '|';
                        values[c] = 1;
                        prevValues[c] = 1;
                    }
                    if (lines[l][c] == '^' && lines[l - 1][c] == '|')
                    {
                        if (c - 1 >= 0)
                        {
                            if (ca[c - 1] != '|')
                                ca[c - 1] = '|';
                            values[c - 1] += prevValues[c];
                        }
                        if (c + 1 < line.Length)
                        {
                            if (ca[c + 1] != '|')
                                ca[c + 1] = '|';
                            values[c + 1] += prevValues[c];
                        }
                        _result++;
                    }
                    if (lines[l][c] == '.' && lines[l - 1][c] == '|')
                    {
                        ca[c] = '|';
                        values[c] += prevValues[c];
                    }
                }
                lines[l] = new string(ca);

                long lineValue = 0;
                for (var v = 0; v < values.Length; v++)
                {
                    lineValue += values[v];
                    prevValues[v] = values[v];
                    values[v] = 0;
                }

                timelines = lineValue > timelines ? lineValue : timelines;
            }

            Logger.Log($"timelines: {timelines}");
            _resultTwo = timelines;
        }
    }
}
