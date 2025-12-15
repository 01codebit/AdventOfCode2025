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
            // Logger.Log($"[Program.Run]\n{string.Join('\n', _positions)}");

            InitDistances();
            InitCircuits();
            FindCircuits(10);

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        // private static double _threshold = 100.0;

        private static void InitDistances()
        {
            Logger.LogLine($"[InitDistances]");
            int skipped = 0;
            double max = 0,
                min = 100000;
            for (var a = 0; a < _positions.Count - 1; a++)
            {
                Logger.LogLine($"[InitDistances] point A {a}/{_positions.Count}");
                for (var b = a + 1; b < _positions.Count; b++)
                {
                    if (b == a)
                        continue;

                    // if (
                    //     _positions[b].X - _positions[a].X > _threshold
                    //     || _positions[b].Y - _positions[a].Y > _threshold
                    //     || _positions[b].Z - _positions[a].Z > _threshold
                    // )
                    // {
                    //     skipped++;
                    //     continue;
                    // }
                    var seg = new Segment(a, b, ComputeDistance(_positions[a], _positions[b]));
                    if (seg.Value > max)
                        max = seg.Value;
                    else if (seg.Value < min)
                        min = seg.Value;
                    InsertOrdered(seg);
                }
            }

            Logger.LogLine($"[InitDistances] found {_segments.Count} segments");
            Logger.LogLine($"[InitDistances] skipped nodes {skipped} - max: {max} - min: {min}");
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

        private static void InitCircuits()
        {
            Logger.LogLine($"[InitCircuits]");
            for (var i = 0; i < _positions.Count; i++)
            {
                _positions[i].Circuit = i;
            }
        }

        private static void FindCircuits(int limit)
        {
            Logger.LogLine($"[FindCircuits]");
            limit = limit < _segments.Count ? limit : _segments.Count;

            for (var i = 1; i < limit; i++)
            {
                var curr = _segments[i];

                var circuitId = _positions[curr.A].Circuit;
                var oldCircuitId = _positions[curr.B].Circuit;
                _positions[curr.B].Circuit = circuitId;

                foreach (var pos in _positions)
                {
                    if (pos.Circuit == oldCircuitId)
                    {
                        pos.Circuit = circuitId;
                    }
                }
            }

            Dictionary<int, List<int>> circuits = [];
            for (var i = 0; i < _positions.Count; i++)
            {
                var pos = _positions[i];
                if (!circuits.ContainsKey(pos.Circuit))
                    circuits.Add(pos.Circuit, []);
                circuits[pos.Circuit].Add(i);
            }

            long[] largest3 = [0, 0, 0];
            var nodesNumberCheck = 0;
            Logger.LogLine($"[FindCircuits] found {circuits.Keys.Count} circuits");
            foreach (var ck in circuits.Keys)
            {
                var cv = circuits[ck];
                nodesNumberCheck += cv.Count;

                if (cv.Count > largest3[0])
                {
                    largest3[2] = largest3[1];
                    largest3[1] = largest3[0];
                    largest3[0] = cv.Count;
                }
                else if (cv.Count > largest3[1])
                {
                    largest3[2] = largest3[1];
                    largest3[1] = cv.Count;
                }
                else if (cv.Count > largest3[2])
                    largest3[2] = cv.Count;
            }
            Logger.LogLine($"[FindCircuits] Largest 3: {largest3[0]} {largest3[1]} {largest3[2]}");

            if (nodesNumberCheck != _positions.Count)
            {
                Logger.LogError(
                    $"[FindCircuits] nodesNumberCheck ({nodesNumberCheck}) must be {_positions.Count}"
                );
            }

            _result = largest3[0] * largest3[1] * largest3[2];
        }

        private static long ComputeDistance(Position a, Position b)
        {
            var dx2 = (a.X - b.X) * (a.X - b.X); // Math.Pow(a.X - b.X, 2);
            var dy2 = (a.Y - b.Y) * (a.Y - b.Y); // Math.Pow(a.Y - b.Y, 2);
            var dz2 = (a.Z - b.Z) * (a.Z - b.Z); //Math.Pow(a.Z - b.Z, 2);
            var ddd = dx2 + dy2 + dz2;
            var dist = (long)Math.Sqrt(ddd);
            return dist;
        }

        public static long IntegerSqrt(long n)
        {
            if (n == 0 || n == 1) return n;
            long low = 1, high = n;
            while (low <= high)
            {
                long mid = low + (high - low) / 2;
                if (mid <= n / mid) // Avoid overflow
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return high;
        }

        private static void PartOneCount() { }

        private static void PartTwoCount() { }
    }
}
