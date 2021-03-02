using CouchDB.Driver;
using CouchDB.Driver.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace StellarPointer.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services,
            byte[] signingKey)
        {
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator
                };
            });

            return services;
        }

        public static IServiceCollection AddCouchContext<TContext>(this IServiceCollection services,
            Action<CouchOptionsBuilder<TContext>> optionBuilderAction)
            where TContext : CouchContext
        {
            NotNull(services, nameof(services));
            NotNull(optionBuilderAction, nameof(optionBuilderAction));

            var builder = new CouchOptionsBuilder<TContext>();
            optionBuilderAction?.Invoke(builder);
            return services
                .AddSingleton(builder.Options)
                .AddSingleton<TContext>();
        }

        private static bool LifetimeValidator(DateTime? notBefore,
            DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            return expires != null && expires > DateTime.Now;
        }

        private static void NotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
