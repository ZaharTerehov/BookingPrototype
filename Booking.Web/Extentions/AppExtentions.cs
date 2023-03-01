using Booking.ApplicationCore.Enum;
using Booking.Web.Interfaces.Login;
using Booking.Web.Middleware;
using Booking.Web.Services.Account;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Booking.Web.Extentions
{
    public static class AppExtentions
    {
        public static IList<SelectListItem> SetSelectedValue(this IList<SelectListItem> collection, int? id)
        {
            if (id != null)
            {
                var item = collection.FirstOrDefault(item => item.Value != null && item.Value.Equals(id.ToString()));
                if (item is not null)
                    item.Selected = true;
            }
            
            return collection;
        }

        public static IList<SelectListItem> AddAllItem(this IList<SelectListItem> collection, bool itemAllSelected = true)
        {
            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = itemAllSelected };
            collection.Insert(0, allItem);
            return collection;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration,
            JwtOptions tokenOptions)
        {
			services.AddScoped<ITokenService, TokenService>(serviceProvider =>
	            new TokenService(tokenOptions));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }) 
            .AddCookie()
            .AddGoogleOpenIdConnect(options =>
            {
                options.ClientId = configuration.GetValue<string>("GoogleOpenIdConnect:ClientId");
                options.ClientSecret = configuration.GetValue<string>("GoogleOpenIdConnect:ClientSecret");
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,

                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,

					ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenOptions.SigningKey)),

				    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireRole(nameof(Role.User)));
                options.AddPolicy("Admin", policy => policy.RequireRole(nameof(Role.Admin)));
            });

            return services;
        }

        public static IApplicationBuilder UseSecureJwt(this IApplicationBuilder builder) => builder.UseMiddleware<SecureJwtMiddleware>();
    }
}
