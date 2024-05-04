namespace CarUsage.Core.ResultPattern;

public interface IResult //void donen metotlar için dönüş değeri
{
    public bool Success { get; }
    public string? Message { get; }
}