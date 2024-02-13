using System.Net;
using System.Text;

namespace Using_http_client.Utilities;

public class CustomHandler : DelegatingHandler
{
    public CustomHandler()
        : base(new HttpClientHandler())
    {
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Before sending request!");
        //Set Headers for example
        //RefreshToken(); //For Example
        request.Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}", Encoding.UTF8, "application/json");//CONTENT-TYPE header

        var response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("After sending request!");

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            Console.Write("User is unauthorized!");
        }

        Console.WriteLine("Hooray Authorized");

        return response;
    }

}