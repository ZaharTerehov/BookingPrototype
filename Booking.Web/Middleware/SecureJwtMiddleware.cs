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
            var token = context.Request.Cookies[accountServiceViewModelService.LocationAccessToken];
            var refreshToken = context.Request.Cookies[accountServiceViewModelService.LocationRefreshToken];

            if (!string.IsNullOrEmpty(token))
            {
                if (await accountServiceViewModelService.CheckValidUser(token))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                else
                {
                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        var validToken = await accountServiceViewModelService.UpdateUserValidity(token, refreshToken);

                        if (validToken != null)
                        {
                            context.Response.Cookies.Append(accountServiceViewModelService.LocationAccessToken, validToken.AccessToken,
                            new CookieOptions
                            {
                                Expires = validToken.AccessTokenExpires
                            });
                            
                            context.Request.Headers.Add("Authorization", "Bearer " + validToken.AccessToken.ToString());
                        }
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
