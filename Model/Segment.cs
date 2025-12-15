using Common;

namespace Model;

public struct Segment(int indexA, int indexB, long value)
{
    public int A { get; set; } = indexA;
    public int B { get; set; } = indexB;
    public long Value { get; set; } = value;

    public override string ToString()
    {
        return $"Segment: {A}-{B} ({Value})";
    }
}
