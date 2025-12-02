using Model;

namespace Day_01
{
    public class Program : Common.IRunnablePart
    {
        private static string _defaultFileName = "input_test.txt";
        private static List<Rotation> _rotations = new();
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
            var fn = _defaultFileName;
            if (args.Length > 0)
            {
                fn = args[0];

                if (args.Length > 1)
                {
                    _debug = args[1] == "debug";
                }
            }

            var currentFolder = Directory.GetCurrentDirectory() + "\\01\\";
            var filePath = Path.Combine(currentFolder, fn);
            _rotations = await Utils.FileUtils.ReadTextFileAsync<Rotation>(
                filePath,
                new[] { '\r', '\n' },
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
                Console.WriteLine($"Part Two --------------------------------------");
            if (_debug)
                Console.WriteLine($"  - The dial starts by pointing at {_currentValue}.");

            foreach (var rotation in _rotations)
            {
                var sign = rotation.Direction == EDirection.Left ? -1 : 1;

                var prevValue = _currentValue;
                _currentValue += sign * rotation.Value % _maxValue;
                var passes = rotation.Value / _maxValue;

                if (_currentValue < 0)
                {
                    if (prevValue > 0)
                        passes++;
                    _currentValue += _maxValue;
                }
                else if (_currentValue > _maxValue)
                {
                    passes++;
                    _currentValue %= _maxValue;
                }
                else if (_currentValue == _maxValue)
                {
                    _currentValue %= _maxValue;
                }

                _result += passes;

                if (_debug)
                {
                    if (passes > 0)
                        Console.WriteLine(
                            $"  - The dial is rotated {rotation} to point at {_currentValue}; during this rotation it points at 0 {passes}."
                        );
                    else
                        Console.WriteLine(
                            $"  - The dial is rotated {rotation} to point at {_currentValue}."
                        );
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
