using System.Net;

namespace City_Transportation_Systems.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenMiddleware(RequestDelegate next, IConfiguration confugiration)
        {
            _next = next;
            _configuration = confugiration;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Headers["Authorization"];
            string staticToken = _configuration["TokenSettings:StaticToken"];

            if (token == null || token != $"Bearer {staticToken}")
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await Console.Out.WriteLineAsync($"got token: {token}");
                await Console.Out.WriteLineAsync($"----------------------------");
                await Console.Out.WriteLineAsync($"static token : {staticToken}");
                await context.Response.WriteAsync("Invalid token.");
                return;
            }

            await _next(context);
        }

    }
}
