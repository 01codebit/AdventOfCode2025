namespace Common;

public interface IRunnablePart
{
    static abstract Task Run(string[] args);
}
