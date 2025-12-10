using System.Collections.Generic;
using System.Numerics;
using Common;
using Model;
using Utils;

namespace Day_08
{
    public class Playground : IRunnablePart
    {
        private static long _result = 0;
        private static BigInteger _resultTwo = 0;
        private static List<Position> _positions = [];
        private static List<Segment> _segments = [];

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            _positions = await FileUtils.ReadListFromFileAsync<Position>(filePath, ['\n', '\r']);
            Logger.Log($"[Program.Run]\n{string.Join('\n', _positions)}");

            InitDistances();

            for (var o = 0; o < 20; o++)
            {
                var s = _segments[o];
                Logger.LogLine($"segment #{o}: {s} [{_positions[s.A]} - {_positions[s.B]}]");
            }

            FindCircuits(10);

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void InitDistances()
        {
            for (var a = 0; a < _positions.Count - 1; a++)
            {
                for (var b = a + 1; b < _positions.Count; b++)
                {
                    if (b == a)
                        continue;
                    var seg = new Segment(a, b, ComputeDistance(_positions[a], _positions[b]));
                    InsertOrdered(seg);
                }
            }
        }

        private static void InsertOrdered(Segment s)
        {
            for (var i = 0; i < _segments.Count; i++)
            {
                if (_segments[i].Value > s.Value)
                {
                    _segments.Insert(i, s);
                    return;
                }
            }
            _segments.Add(s);
        }

        private static void FindCircuits(int limit)
        {
            List<List<Segment>> circuits = [];
            circuits.Add([_segments[0]]);
            Logger.Log($"added new list with segment: {_segments[0]}");

            limit = limit < _segments.Count ? limit : _segments.Count;

            var found = -1;
            for (var i = 1; i < limit; i++)
            {
                var curr = _segments[i];
                for (var j = 0; j < circuits.Count; j++)
                {
                    foreach (var segment in circuits[j])
                    {
                        if (
                            curr.A == segment.A
                            || curr.A == segment.B
                            || curr.B == segment.A
                            || curr.B == segment.B
                        )
                        {
                            found = j;
                            break;
                        }
                    }
                    if (found >= 0)
                    {
                        circuits[j].Add(curr);
                        Logger.Log($"added segment: {curr}");
                        found = -1;
                        break;
                    }
                    else
                    {
                        circuits.Add([curr]);
                        Logger.Log($"added new list with segment: {curr}");
                    }
                }
            }

            int[] largest3 = new int[3];
            largest3[0] = 0;
            largest3[1] = 0;
            largest3[2] = 0;
            var kkk = 0;
            foreach (var c in circuits)
            {
                Logger.LogLine($"Circuit: ({c.Count})[{string.Join(',', c)}]");
                kkk += c.Count;

                if (c.Count > largest3[0])
                    largest3[0] = c.Count;
                else if (c.Count > largest3[1])
                    largest3[1] = c.Count;
                else if (c.Count > largest3[2])
                    largest3[2] = c.Count;
            }
            Logger.LogLine($"largest: {largest3[0]} {largest3[1]} {largest3[2]}");
            Logger.LogLine($"kkk: {kkk}");

            _result = largest3[0] * largest3[1] * largest3[2];
        }

        private static double ComputeDistance(Position a, Position b)
        {
            var dx2 = Math.Pow(a.X - b.X, 2);
            var dy2 = Math.Pow(a.Y - b.Y, 2);
            var dz2 = Math.Pow(a.Z - b.Z, 2);
            var dist = Math.Sqrt(dx2 + dy2 + dz2);
            return dist;
        }

        private static void PartOneCount() { }

        private static void PartTwoCount() { }
    }
}
