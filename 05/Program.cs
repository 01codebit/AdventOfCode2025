using System.Numerics;
using Common;
using Model;
using Utils;

namespace Day_05
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static BigInteger _resultTwo = 0;

        private static IngredientsDB _db;

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            var input = await FileUtils.ReadTextFileAsync(filePath);
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

        private static void PartTwoCount()
        {
            Logger.LogLine($"---- Before reduction: {_db.Ranges.Count} ----");
            ReduceRanges();

            Logger.LogLine($"---- Result: {_db.Ranges.Count} ----");
            foreach (var r in _db.Ranges)
            {
                Logger.Log($"  range: {r}: {r.End - r.Start + 1}");
                _resultTwo += r.End - r.Start + 1;
            }
        }


        private static List<LongRange> CopyDBList()
        {
            List<LongRange> result = [];
            foreach (var r in _db.Ranges)
            {
                var x = new LongRange(r.Start, r.End);
                result.Add(x);
            }
            return result;
        }

        private static void ReduceRanges()
        {
            // List<LongRange> copy = []; //CopyDBList();
            for (var i = 0; i < _db.Ranges.Count; i++)
            {
                var rangeA = _db.Ranges[i];
                Logger.Log($"  rangeA: {rangeA}");

                for (var j = 0; j < _db.Ranges.Count; j++)
                {
                    if (i == j)
                        continue;

                    var rangeB = _db.Ranges[j];
                    Logger.Log($"    rangeA: {rangeA} - rangeB: {rangeB}");

                    if (CompareRanges(ref rangeA, rangeB))
                    {
                        Logger.Log($"    modified rangeA: {rangeA}");
                        _db.Ranges[i] = rangeA;
                        _db.Ranges.RemoveAt(j);
                    }
                }

                // copy.Add(rangeA);
            }

            // _db.Ranges = copy;
        }

        private static bool CompareRanges(ref LongRange a, LongRange b)
        {
            bool result = false;

            if (a.End >= b.Start && a.End <= b.End)
            {
                a.End = b.End;
                result = true;
            }
            if (a.Start <= b.End && a.Start >= b.Start)
            {
                a.Start = b.Start;
                result = true;
            }

            return result;
        }
    }
}
