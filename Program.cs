using System;
using Day_01;

namespace AOC2025
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("[AOC2025.Start] -------------------------------");

            if (args.Length < 1)
            {
                Console.WriteLine("Please provide the day number as a command-line argument.");
            }
            else
            {
                var day = args[0];
                Console.WriteLine($"[Program.Main] Running Day {day} solution...");

                string[] fnArgs = args.Skip(1).ToArray();

                int msg;
                Int32.TryParse(day, out msg);
                switch (msg)
                {
                    case 1:
                        await Day_01.Program.Run(fnArgs);
                        break;
                    // Add additional days here as they are implemented
                    case 2:
                        await Day_02.Program.Run(fnArgs);
                        break;
                    default:
                        Console.WriteLine(
                            "Invalid day number provided or day not yet implemented."
                        );
                        break;
                }
            }

            Console.WriteLine("[AOC2025.End] ---------------------------------");
        }
    }
}
