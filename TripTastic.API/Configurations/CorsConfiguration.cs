
namespace TripTastic.API.Configurations;

public static class CorsConfiguration
{
    public const string PolicyName = "TripTasticAngularPolicy";

    public static IServiceCollection AddTripTasticCors(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}
