using Utils;

namespace AOC2025
{
    public static class MainProgram
    {
        private static string _defaultFileName = "input_test.txt";

        public static async Task Main(string[] args)
        {
            Logger.LogLine("[AOC2025.Start] -------------------------------");

            if (args.Length < 1)
            {
                Logger.LogLine("Please provide the day number as a command-line argument.");
            }
            else
            {
                var day = args[0];
                Logger.LogLine($"[MainProgram.Main] Running Day {day} solution...");

                _ = int.TryParse(day, out int msg);

                var fn = args.Length > 1 ? args[1] : _defaultFileName;
                var debug = args.Length > 2 ? args[2] : "";

                if (debug == "--debug" || debug == "-d")
                {
                    Logger.Debug = true;
                }

                var currentFolder = Directory.GetCurrentDirectory();

                string paddedDay = day.ToString().PadLeft(2, '0'); // Result: "00"
                if (!currentFolder.EndsWith($"\\{paddedDay}\\"))
                    currentFolder += $"\\{paddedDay}\\";

                Logger.LogLine($"[MainProgram.Main] Current folder: {currentFolder}");
                var filePath = Path.Combine(currentFolder, fn);

                string[] fnArgs = [filePath];

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
                    case 4:
                        await Day_04.Program.Run(fnArgs);
                        break;
                    case 5:
                        await Day_05.Program.Run(fnArgs);
                        break;
                    case 6:
                        await Day_06.Program.Run(fnArgs);
                        break;
                    case 7:
                        await Day_07.Program.Run(fnArgs);
                        break;
                    // Add additional days here as they are implemented
                    default:
                        Logger.LogError("Invalid day number provided or day not yet implemented.");
                        break;
                }
            }

            Logger.LogLine("[AOC2025.End] ---------------------------------");
        }
    }
}
