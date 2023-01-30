namespace Booking.Web.Middleware
{
	public class SecureJwtMiddleware
	{
		private readonly RequestDelegate _next;

		public SecureJwtMiddleware(RequestDelegate next) => _next = next;

		public async Task InvokeAsync(HttpContext context) 
		{
			var token = context.Request.Cookies["Booking.Application.Id"];

			if(!string.IsNullOrEmpty(token)) {
				context.Request.Headers.Add("Authorization", "Bearer " + token);
			}

            // To protect against MIME type vulnerabilities
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            // Forced XSS filtering
            context.Response.Headers.Add("X-Xss-Protection", "1");
            // Protect against clickjacking attempts
            context.Response.Headers.Add("X-Frame-Options", "DENY");

			await _next(context);
		}
	}
}
