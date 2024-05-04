namespace CarUsage.Core.ResultPattern;

public class DataResult<T>: Result, IDataResult<T> 
{
    public DataResult(bool success) : base(success)
    {
    }
    
    public DataResult(bool success, string message) : base(success, message)
    {
    }
    public DataResult(T data, bool success) : base(success)
    {
        Data = data;
    }

    public DataResult(T data, bool success, string message) : base(success, message)
    {
        Data = data;
    }

    public T? Data { get; }
}