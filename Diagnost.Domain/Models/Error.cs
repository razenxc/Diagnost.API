namespace Diagnost.Domain.Models;

public class Error
{
    public bool IsError { get; private set; }
    public string? Message { get; private set; }

    public Error(bool status, string? message = null)
    {
        Message = null;
        
        if (status == true)
        {
            IsError = true;
            Message = message;
        }
        else if (status == false)
        {
            IsError = false;
        }
    }
}