using Common;

namespace Model;

public class Point2D(long xcoord, long ycoord) : IReadableString<Point2D>
{
    public long X { get; set; } = xcoord;
    public long Y { get; set; } = ycoord;

    public static Point2D FromString(string input)
    {
        var tokens = input.Split(',');
        if (tokens.Length < 2)
        {
            throw new ArgumentException("Invalid Point2D string");
        }

        long x = long.Parse(tokens[0]);
        long y = long.Parse(tokens[1]);

        return new Point2D(x, y);
    }

    public override string ToString()
    {
        return $"Point2D: ({X}, {Y})";
    }
}
