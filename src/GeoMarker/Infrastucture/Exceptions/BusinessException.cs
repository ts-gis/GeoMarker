namespace GeoMarker.Infrastucture.Exceptions;

public class BusinessException : Exception
{
    public int Code { get; }

    public BusinessException(int code, string message) : base(message)
    {
        Code = code;
    }

    public BusinessException(int code, string message, Exception inner) : base(message, inner)
    {
        Code = code;
    }
}