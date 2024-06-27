using Microsoft.OpenApi.Models;

namespace BuberDiner.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddSwaggerDoc();
        return services;
    }

    private static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "MedHub-Backend.Api", Version = "1.0" });

            // rename methods in swagger doc to start with controller name
            options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

            // configure jwt token bearer authentication documentation for swagger ui
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Add JWT Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            // possible bug - when using swagger ui with a different port number than the one noted below all requests will show CORS error
            options.AddServer(new OpenApiServer { Url = "http://localhost:5069", Description = "Local server" });
        });

        return services;
    }
}