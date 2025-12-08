namespace Models;

public class Problem
{
    public List<string> Numbers = [];
    public char Operand;

    public long Result()
    {
        long r = Operand == '+' ? 0 : 1;
        foreach (var n in Numbers)
        {
            var nn = long.Parse(n);
            if (Operand == '+')
                r += nn;
            else if (Operand == '*')
                r *= nn;
        }
        return r;
    }

    public override string ToString()
    {
        return $"Problem [{string.Join(',', Numbers)}] ['{Operand}'] Result: {Result()}";
    }
}
