namespace CarUsage.Core.ResultPattern;

public interface IDataResult<T> : IResult // geriye data döndüğümüz result değeri
{
    public T? Data { get; }
}