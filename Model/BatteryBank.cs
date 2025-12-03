using System.Text;
using Common;

namespace Model;

public struct BatteryBank(int size, int[] cells) : IReadableString<BatteryBank>
{
    public int Size { get; set; } = size;

    public int[] Cells { get; set; } = cells;

    public static BatteryBank FromString(string input)
    {
        var length = input.Length;
        var cells = new int[length];
        for (int i = 0; i < length; i++)
        {
            cells[i] = input[i] - '0';
        }

        return new BatteryBank(length, cells);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var cell in Cells)
        {
            sb.Append(cell.ToString());
        }
        return sb.ToString();
    }
}
