using Common;

namespace Model;

public struct Segment(int indexA, int indexB, double value)
{
    public int A { get; set; } = indexA;
    public int B { get; set; } = indexB;
    public double Value { get; set; } = value;

    public override string ToString()
    {
        return $"Segment: {A}-{B} ({Value})";
    }
}

public struct Position(int x, int y, int z) : IReadableString<Position>
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Z { get; set; } = z;

    public static Position FromString(string input)
    {
        var tokens = input.Split(',');
        if (tokens.Length < 3)
        {
            throw new ArgumentException("Invalid position string");
        }

        int x = int.Parse(tokens[0]);
        int y = int.Parse(tokens[1]);
        int z = int.Parse(tokens[2]);

        return new Position(x, y, z);
    }

    public override string ToString()
    {
        return $"Position: ({x}, {y}, {z})";
    }
}
