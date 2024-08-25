using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.FrontendUI.Entities.Exceptions;

public class ApiException : ApplicationException
{
    public ApiError Error { get; set; }
    public ApiException(string message) : base(message)
    {
        Error = new ApiError(message);
    }
    public ApiException(ApiError error) : base(error.ErrorMessage)
    {
        Error = error;
    }
}

public class ApiError
{
    public string ErrorMessage { get; set; }
    public string ErrorPropertyName { get; set; }

    public ApiError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
