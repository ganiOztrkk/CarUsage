namespace CarUsage.Core.ResultPattern;

public class SuccessDataResult<T> : DataResult<T> 
{
    public SuccessDataResult() : base(true)
    {
    }
    
    public SuccessDataResult(string message) : base(true, message)
    {
    }
    public SuccessDataResult(T data) : base(data, true)
    {
    }

    public SuccessDataResult(T data, string message) : base(data, true, message)
    {
    }
}