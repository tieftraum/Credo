using System;
using Credo.Domain.Interfaces;
using Credo.Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;

namespace Credo.API.Installers
{
    public class JwtInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettings);
            services.AddOptions<JwtSettings>().Bind(configuration);

            services.AddAuthentication(authOpts =>
            {
                authOpts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOpts =>
            {
                bearerOpts.SaveToken = true;
                bearerOpts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings[nameof(JwtSettings.Secret)])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


            //The reason of why swagger installation into service collection happens
            //here is because I wanted to be able to use swagger for authentication
            //and authorization and it was handy to place it here.
            services.AddSwaggerGen(swagOpts =>
            {
                swagOpts.SwaggerDoc("v1", new OpenApiInfo { Title = "Credo Bank Example", Version = "v1" });

                swagOpts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Done in order to be able to authenticate vie SWAGGER",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                swagOpts.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });

        }
    }
}
