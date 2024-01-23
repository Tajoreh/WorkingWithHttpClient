using _02_HttpClient;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<ApplicationSettingOptions>();

//v2
//builder.Services.AddHttpClient();

//v3 : named client
builder.Services.AddHttpClient("dummyClient", (serviceProvider, httpClient) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;

    httpClient.BaseAddress = new Uri(settings.BaseUrl);

});

//v4: typed client
builder.Services.AddHttpClient<DummyServices>((serviceProvider, httpClient) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;

    httpClient.BaseAddress = new Uri(settings.BaseUrl);

})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(5)
});


var app = builder.Build();

app.UseHttpsRedirection();

app.MapUserEndpoints();

app.Run();

