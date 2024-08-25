using AlFikr.FrontendUI.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlFikr.FrontendUI.Web.Areas;

public class PageModelBase: PageModel
{
    private readonly IConfiguration _configuration;
    public string ErrorText { get; set; }
    public string SuccessText { get; set; }

    public PageModelBase(IConfiguration configuration = null)
    {
        _configuration = configuration;
    }

    public void ShowAlertError(string message)
    {
        ErrorText = message;
    }

    public IActionResult HandleError(string errorMessage)
    {
        return HandleError(new ApiException(errorMessage));
    }

    public IActionResult HandleError(ApiException ex, string propName = null, Func<string> func = null)
    {
        if (!string.IsNullOrEmpty(ex.Error.ErrorPropertyName))
        {
            ModelState.AddModelError($"{propName}.{ex.Error.ErrorPropertyName}", ex.Error.ErrorMessage);
        }
        else
        {
            if(func != null)
            {
                ShowAlertError($"{ex.Error.ErrorMessage}{Environment.NewLine}{Environment.NewLine}{func?.Invoke()}");
            }
            else
            {
                ShowAlertError($"{ex.Error.ErrorMessage}{Environment.NewLine}{Environment.NewLine}");
            }
        }

        return Page();
    }
}
