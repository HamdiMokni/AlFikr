namespace AlFikr.BookService.Entities.Exceptions;

public class ApiException: ApplicationException
{
    public ApiError Error { get; set; }
    public ApiException(string message): base(message)
    {
        Error = new ApiError(message);
    }
    public ApiException(ApiError error):base(error.ErrorMessage)
    {
        Error = error;
    }
}

public class ApiError
{
    public string ErrorMessage { get; set; }

    public ApiError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
