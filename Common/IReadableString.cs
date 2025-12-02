namespace Common;

public interface IReadableString<T>
{
    static abstract T FromString(string input);
}
