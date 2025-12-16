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
