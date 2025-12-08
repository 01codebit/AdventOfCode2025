namespace Models;

public class Problem
{
    public List<long> Numbers = [];
    public char Operand;

    public long Result()
    {
        long r = Operand == '+' ? 0 : 1;
        foreach (var n in Numbers)
        {
            if (Operand == '+')
                r += n;
            else if (Operand == '*')
                r *= n;
        }
        return r;
    }

    public override string ToString()
    {
        return $"Problem [{string.Join(',', Numbers)}] ['{Operand}'] Result: {Result()}";
    }
}
