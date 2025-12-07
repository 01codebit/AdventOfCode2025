using System.Numerics;
using Common;
using Utils;

namespace Day_06
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
            Logger.Log($"[Program.Run] {input}");

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void PartOneCount()
        {
        }

        private static void PartTwoCount()
        {
        }
    }
}
