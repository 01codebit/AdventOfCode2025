using System;

namespace Utils
{
    public static class Logger
    {
        public static bool Debug = false;

        public static void Log(string message)
        {
            if (Debug)
            {
                Console.WriteLine(message);
            }
        }

        public static void LogLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void LogError(string message)
        {
            Console.WriteLine($"*** ERROR ***: {message}");
        }

        public static void PercentageWrite(int x, int total, string message)
        {
            if (x == total)
            {
                Console.WriteLine();
                return;
            }

            var p = x / total;
            var s = new string('*', p);
            var m = s.PadRight(20, '-');
            Console.Write($"{m} {message} {p:P0}\r");
        }
    }
}
