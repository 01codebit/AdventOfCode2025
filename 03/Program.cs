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

        private static long BankJoltageTwo(BatteryBank bank, int digits = 2)
        {
            if (digits > bank.Size)
            {
                throw new ArgumentException("Digits cannot be greater than bank size");
            }

            long joltage = 0;

            int[] deltas = InitDeltas(digits);
            Log($"Initialized deltas: {string.Join(", ", deltas)}");

            int maxDelta = bank.Size - digits;

            for(var d=0; d<maxDelta; d++)
            {
                var currentMaxDelta = d;
                deltas[digits-1] = currentMaxDelta;

                for(var i=digits-2; i>0; i--)
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
    }
}
