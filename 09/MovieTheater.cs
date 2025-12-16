using System.Numerics;
using Common;
using Utils;

namespace Day_09
{
    public class MovieTheater : IRunnablePart
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
            // var limit = int.Parse(args[1]);

            // _positions = await FileUtils.ReadListFromFileAsync<Position>(filePath, ['\n', '\r']);

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result Two: {_resultTwo}");
        }


        private static void PartOneCount()
        {
        }

        private static void PartTwoCount()
        {
        }
    }
}
