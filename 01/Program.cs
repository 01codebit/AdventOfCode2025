using Common;
using Model;

namespace Day_01
{
    public class Program : IRunnablePart
    {
        private static List<Rotation> _rotations = [];
        private static int _maxValue = 100;
        private static int _startingValue = 50;
        private static int _currentValue = 0;
        private static int _result = 0;
        private static bool _debug = false;

        private static void PrintResult()
        {
            Console.WriteLine($"[Program.PrintResult] Result: {_result}");
        }

        public static async Task Run(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("Please provide the input file path as an argument.");
            }

            var filePath = args[0];
            _debug = args[1] == "debug";

            _rotations = await Utils.FileUtils.ReadListFromFileAsync<Rotation>(
                filePath,
                ['\r', '\n'],
                false
            );

            PartOneCount();
            Console.WriteLine("[Program.Run] Part One:");
            PrintResult();

            _result = 0; // Reset result for part two

            PartTwoCount();
            Console.WriteLine("[Program.Run] Part Two:");
            PrintResult();
        }

        private static void PartOneCount()
        {
            _currentValue = _startingValue;

            foreach (var rotation in _rotations)
            {
                if (rotation.Direction == EDirection.Left)
                {
                    _currentValue -= rotation.Value % _maxValue;
                }
                else if (rotation.Direction == EDirection.Right)
                {
                    _currentValue += rotation.Value % _maxValue;
                }

                if (_currentValue < 0)
                {
                    _currentValue += _maxValue;
                }

                _currentValue %= _maxValue;

                if (_currentValue == 0)
                {
                    _result++;
                }
            }
        }

        private static void PartTwoCount()
        {
            _currentValue = _startingValue;

            if (_debug)
            {
                Console.WriteLine($"Part Two --------------------------------------");
                Console.WriteLine($"  - The dial starts by pointing at {_currentValue}.");
            }

            foreach (var rotation in _rotations)
            {
                var sign = rotation.Direction == EDirection.Left ? -1 : 1;

                var prevValue = _currentValue;
                _currentValue += sign * rotation.Value % _maxValue;
                var passes = rotation.Value / _maxValue;

                if ((_currentValue < 0 && prevValue > 0) || _currentValue > _maxValue)
                    passes++;
                _result += passes;

                if (_currentValue < 0)
                {
                    _currentValue += _maxValue;
                }
                else if (_currentValue >= _maxValue)
                {
                    _currentValue %= _maxValue;
                }

                if (_debug)
                {
                    string msg = $"  - The dial is rotated {rotation} to point at {_currentValue}";
                    msg += passes > 0 ? $"; during this rotation it points at 0 {passes}." : ".";
                    Console.WriteLine(msg);
                }

                if (_currentValue == 0)
                {
                    if (_debug)
                        Console.WriteLine(
                            $"    - The dial is rotated {rotation} to point at {_currentValue}."
                        );
                    _result++;
                }
            }
        }
    }
}
