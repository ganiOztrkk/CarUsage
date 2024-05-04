namespace CarUsage.Core.ResultPattern;

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult() : base(false)
    {
    }
    
    public ErrorDataResult(string message) : base(false, message)
    {
    }
    public ErrorDataResult(T data) : base(data, false)
    {
    }

    public ErrorDataResult(T data, string message) : base(data, false, message)
    {
    }
    
}