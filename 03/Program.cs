using System;
using System.Numerics;
using Common;
using Model;
using Utils;

namespace Day_03
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static BigInteger _resultTwo = 0;

        private static List<BatteryBank> _banks = [];

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            _banks = await Utils.FileUtils.ReadListFromFileAsync<BatteryBank>(
                filePath,
                ['\r', '\n']
            );

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            var digits = 12;

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount(digits);
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");

            if (digits == 2)
            {
                if (_resultTwo == _result)
                {
                    Logger.LogLine("[Program.Run] SUCCESS !!!");
                }
                else
                {
                    Logger.LogLine("[Program.Run] FAILURE !!!");
                }
            }
        }

        private static void PartOneCount()
        {
            foreach (var bank in _banks)
            {
                var j = BankJoltage(bank);
                Logger.Log($"[Program.PartOneCount] Bank {bank} joltage calculated: {j}");
                _result += j;
            }
        }

        private static long BankJoltage(BatteryBank bank)
        {
            long joltage = 0;
            long prevJoltage = 0;
            for (var i = 0; i < bank.Size - 1; i++)
            {
                var a = bank.Cells[i];
                for (var y = i + 1; y < bank.Size; y++)
                {
                    var b = bank.Cells[y];
                    joltage = Math.Max(prevJoltage, 10 * a + b);
                    prevJoltage = joltage;
                }
            }

            return joltage;
        }

        private static void PartTwoCount(int digits = 2)
        {
            var b = 0;
            foreach (var bank in _banks)
            {
                b++;
                Logger.Log($"[Program.PartTwoCount] ---- Processing bank #{b}/{_banks.Count}");
                var j = BankJoltageTwo(bank, digits);
                Logger.Log($"[Program.PartTwoCount] Bank {bank} joltage calculated: {j}");
                _resultTwo += j;
            }
        }

        private static int[] InitIndexes(int size, int digits)
        {
            if (digits > size)
            {
                throw new ArgumentException("Digits cannot be greater than size");
            }

            int[] indexes = new int[digits];
            for (var i = 0; i < digits; i++)
            {
                indexes[i] = i;
            }

            return indexes;
        }

        private static Dictionary<int, BigInteger> _powCache = new();

        private static BigInteger Pow10(int exponent)
        {
            if (_powCache.ContainsKey(exponent))
            {
                return _powCache[exponent];
            }

            BigInteger result = BigInteger.Pow(10, exponent);
            _powCache[exponent] = result;
            return result;
        }

        private static BigInteger ComputeJoltageFromIndexes(BatteryBank bank, int[] indexes)
        {
            BigInteger joltage = 0;
            int digits = indexes.Length;

            for (var y = 0; y < digits; y++)
            {
                var currentDigit = bank.Cells[indexes[y]];

                joltage += currentDigit * Pow10(digits - y - 1);
            }

            return joltage;
        }

        private static BigInteger BankJoltageTwo(BatteryBank bank, int digits = 2)
        {
            if (digits > bank.Size)
            {
                throw new ArgumentException(
                    $"Digits ({digits}) cannot be greater than bank size ({bank.Size})"
                );
            }

            Logger.Log(
                $"[BankJoltageTwo] ---- Starting calculation for bank {bank} with digits {digits}"
            );

            // init
            int[] indexes = InitIndexes(bank.Size, digits);
            BigInteger joltage = 0;

            int startIndex = 0;
            for (var i = 0; i < indexes.Length; i++)
            {
                int idx = FindMaxBetween(startIndex, bank.Size - (indexes.Length - i), bank.Cells);
                indexes[i] = idx;
                startIndex = idx + 1;
            }

            joltage = ComputeJoltageFromIndexes(bank, indexes);
            Logger.Log($"  [BankJoltageTwo] ---> Return joltage: {joltage}");

            return joltage;
        }

        private static int FindMaxBetween(int start, int end, int[] array)
        {
            int maxIndex = start;
            for (int i = start + 1; i <= end && i < array.Length; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
    }
}
