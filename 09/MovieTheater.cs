using System.Numerics;
using System.Runtime.CompilerServices;
using Common;
using Model;
using Utils;

namespace Day_09
{
    public class MovieTheater : IRunnablePart
    {
        private static long _result = 0;
        private static BigInteger _resultTwo = 0;
        private static List<Point2D> _points = [];

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];
            _points = await FileUtils.ReadListFromFileAsync<Point2D>(filePath, ['\n', '\r']);
            Logger.Log($"[Program.Run] read {_points.Count} points");

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result Two: {_resultTwo}");
        }

        private static long GetArea(Point2D a, Point2D b)
        {
            long dx = a.X - b.X + 1;
            long dy = a.Y - b.Y + 1;
            long area = dx * dy;
            if (area < 0)
                area *= -1;

            return area;
        }

        private static void PartOneCount()
        {
            _result = 0;
            for (var i = 0; i < _points.Count - 1; i++)
            {
                for (var j = i + 1; j < _points.Count; j++)
                {
                    var area = GetArea(_points[i], _points[j]);
                    if (area > _result)
                        _result = area;
                }          
            }
        }

        private static void PartTwoCount()
        {
        }
    }
}
