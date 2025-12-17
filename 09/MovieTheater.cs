using System.Drawing;
using System.Numerics;
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


        private static bool IsCrossing(Point2D a1, Point2D a2, Point2D b1, Point2D b2)
        {
            var result = false;
            if (a1.X == a2.X)
            {
                if (b1.X <= a1.X && b2.X >= a1.X && b1.Y <= a1.Y && b2.Y >= a2.Y)
                    result = true;
            }

            return result;
        }

        private static bool PerimeterCheck(Point2D a, Point2D b)
        {
            var result = false;

            var pointC = new Point2D(a.X, b.Y);
            var pointD = new Point2D(b.X, a.Y);
            var square = new List<Point2D> { a, pointC, b, pointD };

            for (int i = 0, j = square.Count - 1; i < square.Count; j = i++)
            {
                Logger.Log($"[IsPointInPolygon] {i} {j}");

            }

            return result;
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
            _resultTwo = 0;
            for (var i = 0; i < _points.Count - 1; i++)
            {
                for (var j = i + 1; j < _points.Count; j++)
                {
                    Logger.Log($"  points: {_points[i]} {_points[j]}");

                    if (PerimeterCheck(_points[i], _points[j]))
                    {
                        var area = GetArea(_points[i], _points[j]);
                        if (area > _resultTwo)
                        {
                            Logger.Log($"    -> area {area} for points: {_points[i]} {_points[j]}");
                            _resultTwo = area;
                        }
                    }
                }
            }
        }
    }
}
