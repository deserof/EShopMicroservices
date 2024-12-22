namespace BuildingBlocks.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base("Bad Request")
    {
    }
    
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, string details) : base(message)
    {
        Details = details;
    }
    
    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    public string? Details { get; }
}