using Common;
using Model;
using Utils;

namespace Day_02
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static List<LongRange> _ranges = [];

        private static void PrintResult()
        {
            Console.WriteLine($"[Program.PrintResult] Result: {_result}");
        }

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            _ranges = await Utils.FileUtils.ReadListFromFileAsync<LongRange>(filePath, ',');

            PartOneCount();
            Console.WriteLine("[Program.Run] Part One:");
            PrintResult();

            _result = 0; // Reset result for part two

            PartTwoCount();
            Console.WriteLine("[Program.Run] Part Two:");
            PrintResult();
        }

        private static void PartOneCount()
        {
            foreach (var range in _ranges)
            {
                for (var i = range.Start; i <= range.End; i++)
                {
                    Logger.Log($"Counting number: {i}");

                    if (!IsValid(i))
                    {
                        Logger.Log($"  Invalid number found: {i}");
                        _result += i;
                    }
                }
            }
        }

        private static bool IsValid(long value)
        {
            var strValue = value.ToString();
            var length = strValue.Length;
            if (length % 2 != 0)
                return true;

            var mid = length / 2;
            var leftPart = strValue.Substring(0, mid);
            var rightPart = strValue.Substring(mid, mid);
            long left = long.Parse(leftPart);
            long right = long.Parse(rightPart);

            return (left != right);
        }

        private static void PartTwoCount()
        {
            foreach (var range in _ranges)
            {
                for (var i = range.Start; i <= range.End; i++)
                {
                    Logger.Log($"Counting number: {i}");

                    if (!IsValidPartTwo(i))
                    {
                        Logger.Log($"  Invalid number found: {i}");
                        _result += i;
                    }
                }
            }
        }

        private static bool IsValidPartTwo(long value)
        {
            var strValue = value.ToString();
            var length = strValue.Length;
            if (length < 2)
                return true;

            for (var div = 2; div <= length; div++)
            {
                if (length % div != 0)
                    continue;

                var mid = length / div;
                var validCount = 0;
                for (var i = 0; i < div - 1; i++)
                {
                    // take parts of size mid two by two
                    var part = strValue.Substring(i * mid, mid * 2);
                    if (IsValid(long.Parse(part)))
                        break;
                    validCount++;
                }

                if (validCount == div - 1)
                    return false;
            }

            return true;
        }
    }
}
