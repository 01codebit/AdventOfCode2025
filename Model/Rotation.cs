using Common;

namespace Model;

public enum EDirection
{
    Left,
    Right,
}

public struct Rotation(EDirection direction, int value) : IReadableString<Rotation>
{
    public EDirection Direction = direction;
    public int Value = value;

    public static Rotation FromString(string input)
    {
        EDirection direction =
            input[0] == 'L' ? EDirection.Left
            : input[0] == 'R' ? EDirection.Right
            : throw new ArgumentException("Invalid direction");

        int value = int.Parse(input.Substring(1));

        return new Rotation(direction, value);
    }

    public override string ToString()
    {
        var directionChar = Direction == EDirection.Left ? 'L' : 'R';
        return $"{directionChar}{Value}";
    }
}
