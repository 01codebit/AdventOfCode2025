using Common;
using Model;

namespace Day_02
{
    public class Program : Common.IRunnablePart
    {
        private static readonly string _defaultFileName = "input.txt";
        private static long _result = 0;
        private static List<LongRange> _ranges = new List<LongRange>();
        private static bool _debug = false;

        private static void PrintResult()
        {
            Console.WriteLine($"[Program.PrintResult] Result: {_result}");
        }

        public static async Task Run(string[] args)
        {
            var fn = _defaultFileName;
            if (args.Length > 0)
            {
                fn = args[0];

                if (args.Length > 1)
                {
                    _debug = args[1] == "debug";
                }
            }

            var currentFolder = Directory.GetCurrentDirectory() + "\\02\\";
            var filePath = Path.Combine(currentFolder, fn);
            _ranges = await Utils.FileUtils.ReadTextFileAsync<LongRange>(filePath, ',', _debug);

            PartOneCount();
            Console.WriteLine("[Program.Run] Part One:");
            PrintResult();

            _result = 0; // Reset result for part two

            PartTwoCount();
            Console.WriteLine("[Program.Run] Part Two:");
            PrintResult();
        }

        private static async Task ReadFile(string filePath)
        {
            try
            {
                string content = await File.ReadAllTextAsync(filePath);

                content
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .ForEach(line =>
                    {
                        LongRange range = LongRange.FromString(line);
                        if (_debug)
                            Console.WriteLine(range.ToString());
                        _ranges.Add(range);
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }

        private static void PartOneCount()
        {
            foreach (var range in _ranges)
            {
                for (var i = range.Start; i <= range.End; i++)
                {
                    if (_debug)
                        Console.WriteLine($"Counting number: {i}");

                    if (!IsValid(i))
                    {
                        if (_debug)
                            Console.WriteLine($"  Invalid number found: {i}");
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
                    if (_debug)
                        Console.WriteLine($"Counting number: {i}");

                    if (!IsValidPartTwo(i))
                    {
                        if (_debug)
                            Console.WriteLine($"  Invalid number found: {i}");
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
