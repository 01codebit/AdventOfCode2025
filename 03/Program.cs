using System;
using Common;
using Model;

namespace Day_03
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static List<BatteryBank> _banks = [];
        private static bool _debug = false;

        private static void PrintResult()
        {
            Console.WriteLine($"[Program.PrintResult] Result: {_result}");
        }

        public static async Task Run(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];
            _debug = args[1] == "debug";

            _banks = await Utils.FileUtils.ReadListFromFileAsync<BatteryBank>(
                filePath,
                ['\r', '\n'],
                _debug
            );

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
            foreach (var bank in _banks)
            {
                var j = BankJoltage(bank);
                if (_debug)
                    Console.WriteLine(
                        $"[Program.PartOneCount] Bank {bank} joltage calculated: {j}"
                    );
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

        private static void PartTwoCount()
        {
            foreach (var bank in _banks)
            {
                var j = BankJoltageTwo(bank);
                if (_debug)
                    Console.WriteLine(
                        $"[Program.PartTwoCount] Bank {bank} joltage calculated: {j}"
                    );
                _result += j;
            }
        }

        private static void Log(string message)
        {
            if (_debug)
            {
                Console.WriteLine(message);
            }
        }

        private static long BankJoltageTwo_OLD(BatteryBank bank, int digits = 2)
        {
            if (digits > bank.Size)
            {
                throw new ArgumentException("Digits cannot be greater than bank size");
            }

            long joltage = 0;

            int[] deltas = InitDeltas(digits);
            Log($"Initialized deltas: {string.Join(", ", deltas)}");

            int maxDelta = bank.Size - digits;

            for (var d = 0; d < maxDelta; d++)
            {
                var currentMaxDelta = d;
                deltas[digits - 1] = currentMaxDelta;

                for (var i = digits - 2; i > 0; i--)
                {
                    //         for(var toAdd=0; toAdd <= currentMaxDelta; toAdd++)
                    //         {
                    //             deltas[i] = toAdd;
                    //         }

                }
            }

            joltage = ComputeJoltageFromDeltas(bank, deltas);
            Log($"Return joltage: {joltage}");
            return joltage;
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

        private static int[] InitDeltas(int digits)
        {
            int[] deltas = new int[digits];
            for (var i = 0; i < digits; i++)
            {
                deltas[i] = 0;
            }

            return deltas;
        }

        private static long ComputeJoltageFromIndexes(BatteryBank bank, int[] indexes)
        {
            long joltage = 0;
            int digits = indexes.Length;

            for (var y = 0; y < digits; y++)
            {
                var currentDigit = bank.Cells[indexes[y]];
                joltage += currentDigit * (long)Math.Pow(10, digits - y - 1);
            }

            return joltage;
        }

        private static long ComputeJoltageFromDeltas(BatteryBank bank, int[] deltas)
        {
            long joltage = 0;
            int digits = deltas.Length;

            for (var d = 0; d < digits; d++)
            {
                var currentDigit = bank.Cells[d + deltas[d]];
                joltage += currentDigit * (long)Math.Pow(10, digits - d - 1);
            }

            return joltage;
        }


        private static long BankJoltageTwo(BatteryBank bank, int digits = 2)
        {
            if (digits > bank.Size)
            {
                throw new ArgumentException("Digits cannot be greater than bank size");
            }

            Log($"[BankJoltageTwo] Starting calculation for bank {bank} with digits {digits}");

            // init
            long joltage = 0;
            int[] indexes = InitIndexes(bank.Size, digits);
            Log($"Initialized indexes: {string.Join(", ", indexes)}");

            joltage = ComputeJoltageFromIndexes(bank, indexes);

            var currentIndex = indexes.Length - 1;
            var maxIndex = bank.Size - 1;

            while (GoOnComputation(indexes, maxIndex))
            {
                Log($"Indexes at start of loop: {string.Join(", ", indexes)}");

                if (IncrementLastIndex(indexes, maxIndex))
                {
                    Log($"Indexes after IncrementLastIndex: {string.Join(", ", indexes)}");
                    joltage = Math.Max(joltage, ComputeJoltageFromIndexes(bank, indexes));
                }
                else
                {
                    if (IncrementIndexN(indexes, currentIndex))
                    {
                        Log($"Indexes after IncrementIndexN: {string.Join(", ", indexes)}");
                        joltage = Math.Max(joltage, ComputeJoltageFromIndexes(bank, indexes));
                    }
                    else
                    {
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                        }
                    }
                }
            }

            Log($"Return joltage: {joltage}");
            return joltage;
        }

        private static bool GoOnComputation(int[] indexes, int maxIndex)
        {
            if (indexes[indexes.Length - 1] != maxIndex)
                return false;
            
            for (var i = indexes.Length - 2; i > 0; i--)
            {
                if (indexes[i] != indexes[i+1] - 1)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IncrementLastIndex(int[] indexes, int maxIndex)
        {
            var lastPos = indexes.Length - 1;
            if (indexes[lastPos] < maxIndex)
            {
                indexes[lastPos]++;

                for (var i = 0; i < lastPos; i++)
                {
                    indexes[i] = i;
                }

                return true;
            }
            return false;
        }

        private static bool IncrementIndexN(int[] indexes, int currentIndex)
        {
            if (indexes[currentIndex] < indexes[currentIndex + 1])
            {
                indexes[currentIndex]++;
                return true;
            }
            return false;
        }


    }
}