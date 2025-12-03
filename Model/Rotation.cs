using Common;

namespace Model;

public enum EDirection
{
    Left,
    Right,
}

public struct Rotation(EDirection direction, int value) : IReadableString<Rotation>
{
    public EDirection Direction { get; set; } = direction;
    public int Value { get; set; } = value;

    public static Rotation FromString(string input)
    {
        if (input.Length < 2 || (input[0] != 'L' && input[0] != 'R'))
        {
            throw new ArgumentException("Invalid rotation string");
        }

        EDirection direction = input[0] == 'L' ? EDirection.Left : EDirection.Right;

        int value = int.Parse(input[1..]);

        return new Rotation(direction, value);
    }

    public override string ToString()
    {
        var directionChar = Direction == EDirection.Left ? 'L' : 'R';
        return $"{directionChar}{Value}";
    }
}
