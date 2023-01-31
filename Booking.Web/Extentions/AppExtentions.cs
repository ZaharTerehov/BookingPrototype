using Booking.Web.Interfaces.Login;
using Booking.Web.Middleware;
using Booking.Web.Services.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            JwtOptions tokenOptions)
        {
			services.AddScoped<IJwtProvider, JwtProvider>(serviceProvider =>
	            new JwtProvider(tokenOptions));

			services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;

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

            return services;
        }

        public static IApplicationBuilder UseSecureJwt(this IApplicationBuilder builder) => builder.UseMiddleware<SecureJwtMiddleware>();
    }
}
