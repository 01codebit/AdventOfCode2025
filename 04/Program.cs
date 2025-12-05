using System;
using System.Numerics;
using System.Text;
using Common;
using Model;
using Utils;

namespace Day_04
{
    public class Program : IRunnablePart
    {
        private static long _result = 0;
        private static long _resultTwo = 0;

        private static List<string> _map = [];

        public static async Task Run(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];

            _map = await Utils.FileUtils.ReadListFromFileAsync(filePath, ['\r', '\n']);

            Logger.LogLine("[Program.Run] Part One:");
            PartOneCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_result}");

            Logger.LogLine("[Program.Run] Part Two:");
            PartTwoCount();
            Logger.LogLine($"[Program.PrintResult] Result: {_resultTwo}");
        }

        private static void PartOneCount()
        {
            for (var row = 0; row < _map.Count; row++)
            {
                var line = _map[row];
                for (var col = 0; col < line.Length; col++)
                {
                    //Logger.Log($"Processing row {row}, col {col}: {_map[row][col]}");

                    if (_map[row][col] == '@')
                    {
                        int neighbors = 0;
                        // Check all 8 directions
                        for (int dr = -1; dr <= 1; dr++)
                        {
                            for (int dc = -1; dc <= 1; dc++)
                            {
                                if (dr == 0 && dc == 0)
                                    continue; // Skip self

                                int newRow = row + dr;
                                int newCol = col + dc;

                                if (
                                    newRow >= 0
                                    && newRow < _map.Count
                                    && newCol >= 0
                                    && newCol < line.Length
                                    && _map[newRow][newCol] == '@'
                                )
                                {
                                    neighbors++;
                                }
                            }
                        }
                        if (neighbors < 4)
                        {
                            // Logger.Log($"--- FOUND ({row}, {col})");
                            _result++;
                        }
                    }
                }
            }
        }

        private struct Coord(int row, int col)
        {
            public int Row { get; set; } = row;
            public int Col { get; set; } = col;
        }

        private static List<Coord> ToRemove = [];

        private static Dictionary<int, List<int>> ToRemoveByRow = [];


        private static void PartTwoCount()
        {
            while (true)
            {
                int toRemove = 0;
                ToRemove.Clear();
                ToRemoveByRow.Clear();

                for (var row = 0; row < _map.Count; row++)
                {
                    var line = _map[row];
                    for (var col = 0; col < line.Length; col++)
                    {
                        //Logger.Log($"Processing row {row}, col {col}: {_map[row][col]}");

                        if (_map[row][col] == '@')
                        {
                            int neighbors = 0;
                            // Check all 8 directions
                            for (int dr = -1; dr <= 1; dr++)
                            {
                                for (int dc = -1; dc <= 1; dc++)
                                {
                                    if (dr == 0 && dc == 0)
                                        continue; // Skip self

                                    int newRow = row + dr;
                                    int newCol = col + dc;

                                    if (
                                        newRow >= 0
                                        && newRow < _map.Count
                                        && newCol >= 0
                                        && newCol < line.Length
                                        && _map[newRow][newCol] == '@'
                                    )
                                    {
                                        neighbors++;
                                    }
                                }
                            }
                            if (neighbors < 4)
                            {
                                // Logger.Log($"--- FOUND ({row}, {col})");
                                ToRemove.Add(new Coord(row, col));

                                if (!ToRemoveByRow.ContainsKey(row))
                                {
                                    ToRemoveByRow.Add(row, new List<int>());
                                }
                                ToRemoveByRow[row].Add(col);

                                toRemove++;
                            }
                        }
                    }
                }

                if (toRemove == 0)
                    break;

                Logger.Log($"  Found {toRemove} rolls to remove");
                _resultTwo += toRemove;

                // Remove found rolls from map
                foreach (var key in ToRemoveByRow.Keys)
                {
                    string row = _map[key];
                    StringBuilder sb = new StringBuilder(row);

                    foreach (var pos in ToRemoveByRow[key])
                    {
                        sb[pos] = '.'; // index starts at 0!
                    }
                    _map[key] = sb.ToString();
                }
            }
        }
    }
}
