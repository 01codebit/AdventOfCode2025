using System;
using Common;
using Model;
using Utils;

namespace Day_05
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static long _resultTwo = 0;

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            // _map = await Utils.FileUtils.ReadListFromFileAsync(filePath, ['\r', '\n']);

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
