using System;
using Day_01;

namespace AOC2025
{
    public static class Program
    {
        private static string _defaultFileName = "input_test.txt";

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

                _ = int.TryParse(day, out int msg);

                var fn = args.Length > 1 ? args[1] : _defaultFileName;
                var debug = args.Length > 2 ? args[2] : "";

                var currentFolder = Directory.GetCurrentDirectory();

                string paddedDay = day.ToString().PadLeft(2, '0'); // Result: "00"
                if (!currentFolder.EndsWith($"\\{paddedDay}\\"))
                    currentFolder += $"\\{paddedDay}\\";

                Console.WriteLine($"[Program.Main] Current folder: {currentFolder}");
                var filePath = Path.Combine(currentFolder, fn);

                string[] fnArgs = [filePath, debug];

                switch (msg)
                {
                    case 1:
                        await Day_01.Program.Run(fnArgs);
                        break;
                    case 2:
                        await Day_02.Program.Run(fnArgs);
                        break;
                    case 3:
                        await Day_03.Program.Run(fnArgs);
                        break;
                    // Add additional days here as they are implemented
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
