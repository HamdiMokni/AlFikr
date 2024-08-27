using AlFikr.ThesisService.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Net;

namespace AlFikr.ThesisService.Api.Controllers;

public abstract class BaseController<T> : Controller
{
	protected readonly ILogger<T> _logger;

	protected BaseController(ILogger<T> logger)
	{
		_logger = logger;
	}

	protected IActionResult RunAndHandleError(Func<IActionResult> runFunc, string errorMessage)
	{
		return RunAndHandleError(runFunc, errorMessage, null);
	}

	protected IActionResult RunAndHandleError(Func<IActionResult> runFunc, string errorMessage, IDbContextTransaction trans = null)
	{
		try
		{
			return runFunc.Invoke();
		}
		catch (ApiException ex)
		{
			trans?.Rollback();
			_logger.LogError(ex, errorMessage);

			return BadRequest(ex.Error);
		}
		catch (Exception ex)
		{
			trans?.Rollback();
			_logger.LogError(ex, errorMessage);

			if (ex.InnerException == null)
				return StatusCode((int)HttpStatusCode.InternalServerError, new ApiException($"{errorMessage}{Environment.NewLine}{Environment.NewLine}{ex.Message}"));

			if (ex.InnerException is MySqlException || ex.InnerException is ApiException)
				return BadRequest(new ApiException($"{errorMessage}{Environment.NewLine}{Environment.NewLine}{ex.InnerException.Message}"));

			return StatusCode((int)HttpStatusCode.InternalServerError, new ApiException($"{errorMessage}{Environment.NewLine}{Environment.NewLine}{ex.InnerException.Message}"));
		}
		finally
		{
			trans?.Dispose();
		}
	}
}
