using Utils;

namespace Model;

public struct IngredientsDB(List<LongRange> ranges, List<long> ids)
{
    public List<LongRange> Ranges { get; set; } = ranges;

    public List<long> Ids { get; set; } = ids;

    public static IngredientsDB FromString(string input)
    {
        var lines = input.Split('\n');

        var ranges = new List<LongRange>();
        var ids = new List<long>();

        bool changeSection = false;
        foreach (var line in lines)
        {
            if (line == "" || line == "\r")
            {
                changeSection = true;
                continue;
            }
            if (!changeSection)
            {
                var parts = line?.Split('-');
                if (parts != null && parts.Length == 2)
                {
                    var start = long.Parse(parts[0]);
                    var end = long.Parse(parts[1]);
                    ranges.Add(new LongRange(start, end));
                }
            }
            else
            {
                if (line != null)
                {
                    var id = long.Parse(line);
                    ids.Add(id);
                }
            }
        }

        return new IngredientsDB(ranges, ids);
    }

    public override string ToString()
    {
        var text = "IngredientsDB: ";

        text += "[Ranges: ";
        int c = 0;
        foreach (var r in Ranges)
        {
            c++;
            text += $"#{c}: " + r.ToString() + " ";
        }
        text += "]";

        text += "[Ids: ";
        c = 0;
        foreach (var x in Ids)
        {
            c++;
            text += $"#{c}: {x} ";
        }
        text += "]";

        return text;
    }
}