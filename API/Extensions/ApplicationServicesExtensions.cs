using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>(); // AddScoped means it will be disposed after use which is the best (better than singleton which will remain for the shutting down of the app)
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // this is different because we are adding a generic repository

            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                                            .SelectMany(x => x.Value.Errors)
                                                            .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
        
    }
}