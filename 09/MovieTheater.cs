using System.Drawing;
using System.Numerics;
using System.Reflection.PortableExecutable;
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
            long dx = long.Max(a.X, b.X) - long.Min(a.X, b.X) + 1;
            long dy = long.Max(a.Y, b.Y) - long.Min(a.Y, b.Y) + 1;
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
                    {
                        _result = area;
                    }
                }
            }
        }

        private static bool IsPointInsideSquare(Point2D a, Point2D b, Point2D x)
        {
            var maxX = long.Max(a.X, b.X);
            var minX = long.Min(a.X, b.X);
            var maxY = long.Max(a.Y, b.Y);
            var minY = long.Min(a.Y, b.Y);

            if (
                (x.X == a.X && x.Y == a.Y)
                || (x.X == a.X && x.Y == b.Y)
                || (x.X == b.X && x.Y == a.Y)
                || (x.X == b.X && x.Y == b.Y)
            )
                return false;
            else if (x.X >= minX && x.X <= maxX && x.Y >= minY && x.Y <= maxY)
                return true;
            else
                return false;
        }

        private static bool CheckPerimeter(Point2D a, Point2D b)
        {
            for (var i = 0; i < _points.Count; i++)
            {
                if (IsPointInsideSquare(a, b, _points[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckArea(Point2D a, Point2D b)
        {
            for (var i = 0; i < _points.Count; i++)
            {
                var minX = long.Min(a.X, b.X);
                var maxX = long.Max(a.X, b.X);
                var minY = long.Min(a.Y, b.Y);
                var maxY = long.Max(a.Y, b.Y);

                for (var x = minX; x < maxX; x++)
                {
                    for (var y = minY; y < maxY; y++)
                    {
                        if (IsPointInsideSquare(a, b, _points[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static void CreateMap()
        {
            long maxCoord = 0;
            foreach (var p in _points)
            {
                if (p.X > maxCoord)
                {
                    maxCoord = p.X;
                }
                if (p.Y > maxCoord)
                {
                    maxCoord = p.Y;
                }
                if (p.X == p.Y && p.Y == 0)
                {
                    Logger.LogLine($"[PrintMap] found the origin!");
                }
            }
            Logger.LogLine($"[PrintMap] maxCoord: {maxCoord}");

            char[,] map = new char[maxCoord+1, maxCoord+1];
            for (int i = 0; i <= maxCoord; i++)
            {
                for (int j = 0; j <= maxCoord; j++)
                {
                    map[i, j] = '.';
                }
            }

            for (int i = 0, j = _points.Count - 1; i < _points.Count; j = i++)
            {
                var a = _points[i];
                var b = _points[j];
                Logger.Log($":: {i} {j} A:({a.X},{a.Y}) B:({b.X},{b.Y})");
                map[a.X, a.Y] = '#';
                map[b.X, b.Y] = '#';

                for (var x = a.X; x <= b.X; x++)
                {
                    for (var y = a.Y; y <= b.Y; y++)
                    {
                        map[x, y] = '#';
                    }
                }
            }

            Logger.Log("MAP:");
            for (var x = 0; x <= maxCoord; x++)
            {
                for (var y = 0; y <= maxCoord; y++)
                {
                    Console.Write($"{map[x, y]}");
                }
                Console.WriteLine();
            }
        }

        private static void PartTwoCount()
        {
            CreateMap();

            _resultTwo = 0;
            for (var i = 0; i < _points.Count - 1; i++)
            {
                for (var j = i + 1; j < _points.Count; j++)
                {
                    if (CheckPerimeter(_points[i], _points[j])
                        && CheckArea(_points[i], _points[j]))
                    {
                        var area = GetArea(_points[i], _points[j]);
                        // Logger.Log($"    -> area for points {_points[i]} {_points[j]}: {area}");
                        if (area >= _resultTwo)
                        {
                            Logger.Log($"    -> area for points {_points[i]} {_points[j]}: {area}");
                            Logger.Log($"    ---> update {_resultTwo} to new area: {area}");
                            _resultTwo = area;
                        }
                    }
                }
            }
        }
    }
}
