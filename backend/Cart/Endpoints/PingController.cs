using Cart.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Endpoints;

[Route("[controller]")]
public class PingController : Controller
{
    private readonly ISendEndpointProvider sendEndpointProvider;
    private readonly ILogger<PingController> logger;

    public PingController(
        ISendEndpointProvider sendEndpointProvider,
        ILogger<PingController> logger)
    {
        this.sendEndpointProvider = sendEndpointProvider;
        this.logger = logger;
    }
    
    [HttpPost]
    public async Task Post([FromQuery] string message, [FromQuery] bool shouldFail)
    {
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:ping"));
        var command = new PingCommand(message, shouldFail);
        await endpoint.Send(command);
        
        logger.LogInformation(
            "Successfully send ping command with message {Message}", 
            message);
    }
}