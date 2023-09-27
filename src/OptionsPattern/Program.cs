using Managing.NETAppConfigurationWithTheOptionsPattern;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


//Method 1
//builder.Services.Configure<ApplicationOptions>(
//    builder.Configuration.GetSection(nameof(ApplicationOptions)));

//Method 2
builder.Services.ConfigureOptions<ApplicationOptionSetup>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("options", (
    IOptions<ApplicationOptions> options,
        IOptionsSnapshot<ApplicationOptions> optionsSnapshot,
        IOptionsMonitor<ApplicationOptions> optionsMonitor) =>
{
    var response = new
    {
        optionsValue=options.Value.ExampleValue,
        optionsSnapshot=optionsSnapshot.Value.ExampleValue,
        optionsMonitor=optionsMonitor.CurrentValue.ExampleValue
    };

    return Results.Ok(response);
});

app.Run();
