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
            if (Debug)
            {
                Console.WriteLine($"ERROR: {message}");
            }
        }
    }
}
