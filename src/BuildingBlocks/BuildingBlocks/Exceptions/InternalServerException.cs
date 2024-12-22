namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public InternalServerException() : base("Internal server error")
    {
    }
    
    public InternalServerException(string message) : base(message)
    {
    }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }
    
    public InternalServerException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    public string? Details { get; }
}