using Common;

namespace Model;

public struct LongRange(long start, long end) : IReadableString<LongRange>
{
    public long Start { get; set; } = start;
    public long End { get; set; } = end;

    public static LongRange FromString(string input)
    {
        var parts = input.Split('-');
        var start = long.Parse(parts[0]);
        var end = long.Parse(parts[1]);
        return new LongRange(start, end);
    }

    public override string ToString()
    {
        return $"{Start}-{End}";
    }
}
