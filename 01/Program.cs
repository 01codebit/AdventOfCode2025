public enum EDirection
{
    Left,
    Right
}

public struct Rotation(EDirection direction, int value)
{
    public EDirection Direction = direction;
    public int Value = value;

    public override readonly string ToString()
    {
        var directionChar = Direction == EDirection.Left ? 'L' : 'R';
        return $"{directionChar}{Value}";
    }
    
    public static Rotation FromString(string input)
    {
        EDirection direction = input[0] == 'L' ? EDirection.Left 
            : input[0] == 'R' ? EDirection.Right 
            : throw new ArgumentException("Invalid direction");
        
        int value = int.Parse(input.Substring(1));

        return new Rotation(direction, value);
    }
}


public class Program
{
    private static readonly string FilePath = "input.txt";
    private static readonly List<Rotation> Rotations = new();
    private static int _maxValue = 100;
    private static int _startingValue = 50;
    private static int _currentValue = 0;
    private static int _result = 0;
    private static readonly bool _debug = false;


    private static void PrintResult()
    {
        Console.WriteLine($"Result: {_result}");
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("[Start] --------");

        ReadInputFile();

        PartOneCount();
        Console.WriteLine("Part One:");
        PrintResult();

        _result = 0; // Reset result for part two

        PartTwoCount();
        Console.WriteLine("Part Two:");
        PrintResult();

        Console.WriteLine("[End] ----------");
    }

    private static void ReadInputFile()
    {
        Console.WriteLine("Reading input file...");

        try
        {
            string content = File.ReadAllText(FilePath);

            content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .ForEach(line =>
                {
                    Rotation rotation = Rotation.FromString(line);
                    // Console.WriteLine(rotation.ToString());
                    Rotations.Add(rotation);
                });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }
    }

    private static void PartOneCount()
    {
        _currentValue = _startingValue;

        foreach (var rotation in Rotations)
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

            if( _currentValue == 0)
            {
                _result++;
            }
        }
    }

    private static void PartTwoCount()
    {
        _currentValue = _startingValue;
        if(_debug) Console.WriteLine($"Part Two --------------------------------------");
        if(_debug) Console.WriteLine($"  - The dial starts by pointing at {_currentValue}.");

        foreach (var rotation in Rotations)
        {
            var sign = rotation.Direction == EDirection.Left ? -1 : 1;

            var prevValue = _currentValue;
            _currentValue += sign * rotation.Value % _maxValue;
            var passes = rotation.Value / _maxValue;

            if(_currentValue < 0)
            {
                if(prevValue > 0) passes++;
                _currentValue += _maxValue;
            }
            else if(_currentValue > _maxValue)
            {
                passes++;
                _currentValue %= _maxValue;
            }
            else if(_currentValue == _maxValue)
            {
                _currentValue %= _maxValue;
            }

            _result += passes;

            if(_debug) 
            {
                if(passes > 0)
                    Console.WriteLine($"  - The dial is rotated {rotation} to point at {_currentValue}; during this rotation it points at 0 {passes}.");
                else
                    Console.WriteLine($"  - The dial is rotated {rotation} to point at {_currentValue}.");
            }

            if( _currentValue == 0)
            {
                if(_debug) Console.WriteLine($"    - The dial is rotated {rotation} to point at {_currentValue}.");
                _result++;
            }
        }
    }
}