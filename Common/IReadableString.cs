namespace Common;

public interface IReadableString<out T>
{
    static abstract T FromString(string input);
}
