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

        private static IngredientsDB _db;

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            var input = await Utils.FileUtils.ReadTextFileAsync(filePath);
            _db = IngredientsDB.FromString(input);
            Logger.Log($"[Program.Run] {_db}");

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void PartOneCount()
        {
            foreach (var id in _db.Ids)
            {
                foreach (var r in _db.Ranges)
                {
                    if (id >= r.Start && id <= r.End)
                    {
                        _result++;
                        break;
                    }
                }
            }
        }

        private static void PartTwoCount() { }
    }
}
