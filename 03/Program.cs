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

            Console.WriteLine("[Program.Run] Part One:");
            PartOneCount();
            Console.WriteLine($"[Program.PrintResult] Result: {_result}");

            var digits = 12;

            Console.WriteLine("[Program.Run] Part Two:");
            PartTwoCount(digits);
            Console.WriteLine($"[Program.PrintResult] Result: {_resultTwo}");

            if (digits == 2)
            {
                if (_resultTwo == _result)
                {
                    Console.WriteLine("[Program.Run] SUCCESS !!!");
                }
                else
                {
                    Console.WriteLine("[Program.Run] FAILURE !!!");
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
                Console.WriteLine($"[Program.PartTwoCount] ---- Processing bank #{b}/{_banks.Count}");
                var j = BankJoltageTwoAsync(bank, digits);
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

        private static Dictionary<string, BigInteger> _joltageCache = new();

        private static BigInteger Joltage(BatteryBank bank, int[] indexes)
        {
            int[] key = new int[indexes.Length];
            for (var i = 0; i < indexes.Length; i++)
            {
                key[i] = bank.Cells[indexes[i]];
            }
            var keyStr = string.Join("", key);

            if (_joltageCache.ContainsKey(keyStr))
            {
                return _joltageCache[keyStr];
            }

            BigInteger result = ComputeJoltageFromIndexes(bank, indexes);
            _joltageCache[keyStr] = result;
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

        private static BigInteger Factorial(BigInteger number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Number must be non-negative");
            }

            BigInteger result = 1;
            for (BigInteger i = 2; i <= number; i++)
            {
                result *= i;
            }

            Console.WriteLine($"Factorial of {number} is {result}");
            return result;
        }

        public static BigInteger Combinations(BigInteger n, BigInteger k)
        {
            Logger.Log($"[Combinations] Calculating C({n},{k})");
            // if (k > n)
            //     return 0;
            // if (k == 0 || k == n)
            //     return 1;

            // BigInteger num = Factorial(n);
            // BigInteger denom = Factorial(k) * Factorial(n - k);

            // Log($"[Combinations] C({n},{k}) = {num} / {denom} = {num / denom}");
            // return num / denom;

            BigInteger numerator = 1;
            BigInteger denominator = 1;

            for (int i = 0; i < k; i++)
            {
                numerator *= (n - i);
                denominator *= (i + 1);
            }

            return numerator / denominator;
        }

        private static BigInteger BankJoltageTwoAsync(BatteryBank bank, int digits = 2)
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

            // compute the number of combinations of digits numbers in bank.Size
            var combinations = Combinations((BigInteger)bank.Size, (BigInteger)digits);
            Logger.Log($"[BankJoltageTwo] ---- Number of combinations: {combinations}");

            // init
            int[] indexes = InitIndexes(bank.Size, digits);
            BigInteger joltage = 0;
            var maxIndex = bank.Size - 1;
            var count = 0;
            do
            {
                count++;
                Logger.Log(
                    $"    [BankJoltageTwo] #{count} current indexes: [{string.Join(",", indexes)}]"
                );

                joltage = BigInteger.Max(joltage, Joltage(bank, indexes));
            } while (IncrementIndexes(indexes, maxIndex));
            Logger.Log($"  [BankJoltageTwo] made {count} iterations.");
            Logger.Log($"  [BankJoltageTwo] ---> Return joltage: {joltage}");
            return joltage;
        }

        private static bool IncrementIndexes(int[] indexes, int maxIndex)
        {
            var maxFirstIndex = maxIndex - indexes.Length + 1;
            if (indexes[0] == maxFirstIndex)
            {
                return false;
            }

            var currentIndex = indexes.Length - 1;
            for (var i = 0; i < indexes.Length - 1; i++)
            {
                if (indexes[i] < indexes[i + 1] - 1)
                {
                    currentIndex = i;
                    break;
                }
            }
            indexes[currentIndex]++;
            if (currentIndex == indexes.Length - 1 && indexes[currentIndex] > maxIndex)
            {
                return false;
            }

            for (var i = 0; i < currentIndex; i++)
            {
                indexes[i] = i;
            }

            return true;
        }
    }
}
