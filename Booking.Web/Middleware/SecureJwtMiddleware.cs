using Booking.Web.Interfaces;

namespace Booking.Web.Middleware
{
	public class SecureJwtMiddleware
	{
		private readonly RequestDelegate _next;

		public SecureJwtMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context,
            IAccountServiceViewModelService accountServiceViewModelService) 
		{
            var token = context.Request.Cookies["Booking.Application.Id"];
            var refreshToken = context.Request.Cookies["Booking.Application.IdR"];

            if (!string.IsNullOrEmpty(token))
            {
                if (await accountServiceViewModelService.CheckValidUser(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                else
                {
                    var validToken = await accountServiceViewModelService.UpdateUserValidity(token, refreshToken);

                    if(validToken != string.Empty)
                    {
                        context.Response.Cookies.Append("Booking.Application.Id", validToken.ToString(),
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(10)
                        });

                        context.Request.Headers.Add("Authorization", "Bearer " + validToken.ToString());
                    }
                }

                // To protect against MIME type vulnerabilities
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                // Forced XSS filtering
                context.Response.Headers.Add("X-Xss-Protection", "1");
                // Protect against clickjacking attempts
                context.Response.Headers.Add("X-Frame-Options", "DENY");
            }

            await _next(context);
		}
    }
}
