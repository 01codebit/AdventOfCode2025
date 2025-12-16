using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
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
        private static List<Segment> _orderedSegments = [];
        private static Dictionary<int, List<int>> _circuits = [];

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];
            var limit = int.Parse(args[1]);

            _positions = await FileUtils.ReadListFromFileAsync<Position>(filePath, ['\n', '\r']);

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount(limit);
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void InitDistances(int limit)
        {
            Logger.LogLine($"[InitDistances]");
            for (var a = 0; a < _positions.Count - 1; a++)
            {
                Console.Write($"point {a}/{_positions.Count}\r");
                for (var b = a + 1; b < _positions.Count; b++)
                {
                    var seg = new Segment(a, b, ComputeDistance(_positions[a], _positions[b]));
                    _segments.Add(seg);
                }
            }

            Logger.LogLine($"[InitDistances] found {_segments.Count} segments");
            if (_segments.Count < limit)
            {
                Logger.LogError("segments count is too low");
            }
        }

        private static void OrderSegments(int limit)
        {
            var klimit = limit < _segments.Count ? limit : _segments.Count;

            for (int k = 0; k < klimit; k++)
            {
                Console.Write($"[OrderSegments] {k + 1}/{klimit}\r");
                double currentMin = double.MaxValue;
                int currMinIndex = 0;
                for (var i = 0; i < _segments.Count; i++)
                {
                    if (_segments[i].Value < currentMin)
                    {
                        currentMin = _segments[i].Value;
                        currMinIndex = i;
                    }
                }
                _orderedSegments.Add(_segments[currMinIndex]);
                _segments.RemoveAt(currMinIndex);
            }
            Console.WriteLine();
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
            Logger.LogLine($"[FindCircuits] limit: {limit}");
            limit = limit < _orderedSegments.Count ? limit : _orderedSegments.Count;

            for (var i = 1; i < limit; i++)
            {
                var curr = _orderedSegments[i];

                var circuitId = _positions[curr.A].Circuit;
                var oldCircuitId = _positions[curr.B].Circuit;
                _positions[curr.B].Circuit = circuitId;

                var ncount = 2;

                foreach (var pos in _positions)
                {
                    if (pos.Circuit == oldCircuitId)
                    {
                        ncount++;
                        pos.Circuit = circuitId;
                    }
                }
            }

            for (var i = 0; i < _positions.Count; i++)
            {
                var pos = _positions[i];
                if (!_circuits.ContainsKey(pos.Circuit))
                    _circuits.Add(pos.Circuit, []);
                _circuits[pos.Circuit].Add(i);
            }
            Logger.LogLine($"[FindCircuits] found {_circuits.Keys.Count} circuits");
        }

        private static void SelectCircuits()
        {
            long[] largest3 = [0, 0, 0];
            var nodesNumberCheck = 0;
            foreach (var ck in _circuits.Keys)
            {
                var cv = _circuits[ck];
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

        private static double ComputeDistance(Position a, Position b)
        {
            double dist = 0;
            try
            {
                double dx2 = Math.Pow(a.X - b.X, 2);
                double dy2 = Math.Pow(a.Y - b.Y, 2);
                double dz2 = Math.Pow(a.Z - b.Z, 2);
                double ddd = dx2 + dy2 + dz2;
                dist = Math.Sqrt(ddd);
                if (Double.IsNaN(dist))
                {
                    Logger.LogError($"dist is NaN: {dist}");
                    Logger.Log($"dx2: {dx2}, dy2: {dy2}, dz2: {dz2}, ddd: {ddd}");
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }

            return dist;
        }

        private static void PartOneCount(int limit)
        {
            InitDistances(limit);
            OrderSegments(limit);
            InitCircuits();
            FindCircuits(limit);
            SelectCircuits();
        }

        private static void PartTwoCount() { }
    }
}
