using System;
using System.Numerics;
using Common;
using Model;
using Utils;

namespace Day_04
{
    public class Program : IRunnablePart
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

            // _banks = await Utils.FileUtils.ReadListFromFileAsync<BatteryBank>(
            //     filePath,
            //     ['\r', '\n']
            // );

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
        }

        private static void PartTwoCount(int digits = 2)
        {
        }
    }
}
