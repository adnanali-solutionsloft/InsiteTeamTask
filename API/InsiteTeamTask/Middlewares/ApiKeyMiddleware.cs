using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
	private readonly RequestDelegate _next;
	private const string API_KEY_HEADER_NAME = "auth_token";

	public ApiKeyMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
		{
			context.Response.StatusCode = 401;
			return;
		}

		var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

		var apiKey = appSettings.GetValue<string>("ApiKey");

		if (!apiKey.Equals(extractedApiKey))
		{
			context.Response.StatusCode = 401;
			return;
		}

		await _next(context);
	}
}