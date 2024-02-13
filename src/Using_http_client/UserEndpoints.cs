using Microsoft.Extensions.Options;
using Using_http_client.Utilities;

namespace Using_http_client;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("users/v4/{userId}", async (string userId,
            DummyServices dummyServices) =>
        {

            var content = await dummyServices.GetByIdAsync(userId);

            return Results.Ok(content);
        });

        app.MapGet("users/v3/{userId}", async (string userId,
                    IHttpClientFactory factory) =>
                {

                    var httpClient = factory.CreateClient("dummyClient");

                    var content = await httpClient.GetAsync($"/api/v1/employee/{userId}");

                    return Results.Ok(content);
                });

        app.MapGet("users/v2/{userId}", async (string userId,
            IOptions<ApplicationSettings> settings,
           IHttpClientFactory factory) =>
       {

           var httpClient = factory.CreateClient();

           httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);

           //drawback : we have to configure httpClient every time we use it
           var content = await httpClient.GetAsync($"/api/v1/employee/{userId}");

           return Results.Ok(content);
       });

        app.MapGet("users/v1/{userId}", async (string userId,
            IOptions<ApplicationSettings> settings) =>
        {

            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);

            var content = await httpClient.GetAsync($"/api/v1/employee/{userId}");

            return Results.Ok(content);
        });

        app.MapGet("users/v5/{userId}", async (string userId,
            IOptions<ApplicationSettings> settings) =>
        {

            var httpClient = new HttpClient(new CustomHandler());

            httpClient.BaseAddress = new Uri(settings.Value.BaseUrl);

            var content = await httpClient.GetAsync($"/api/v1/employee/{userId}");

            return Results.Ok(content);
        });
    }

}