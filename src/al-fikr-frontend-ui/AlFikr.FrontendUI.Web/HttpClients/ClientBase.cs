using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.Extensions;

namespace AlFikr.FrontendUI.Web.HttpClients;

public class ClientBase
{
	protected static bool InvalidStatusCode(HttpResponseMessage response)
	{
		return response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.NoContent;
	}

	protected void ThrowError(string errorMessage, HttpResponseMessage response)
	{
		if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
		{
			var content = response.Content.ReadAsStringAsync().Result;
			var error = content.ToEntity<ApiError>();

			throw new ApiException(error);
		}

		throw new HttpRequestException(errorMessage, new Exception(response.Content.ReadAsStringAsync().Result), response.StatusCode);
	}

	protected static StringContent CreateJsonContent<T>(T entity)
	{
		string json = entity.ToJson();
		StringContent Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
		return Content;
	}
}
