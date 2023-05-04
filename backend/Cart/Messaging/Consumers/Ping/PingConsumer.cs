using Cart.Contracts;
using Cart.Exceptions;
using MassTransit;

namespace Cart.Messaging.Consumers.Ping;

public class PingConsumer : IConsumer<PingCommand>
{
    private readonly ILogger<PingConsumer> logger;

    public PingConsumer(ILogger<PingConsumer> logger)
    {
        this.logger = logger;
    }
    
    public Task Consume(ConsumeContext<PingCommand> context)
    {
        var command = context.Message; // We use the context to access the typed `Message` property
        
        if (command.ShouldFail)
        {
            logger.LogInformation(
                "Failed to processed {CommandName} with message: {Message}", 
                nameof(PingCommand), 
                command.Message);
            //throw new ShootInFootException();
        }
        
        logger.LogInformation(
            "Successfully processed {CommandName} with message: {Message}", 
            nameof(PingCommand), 
            command.Message);
        
        // If we successfully complete, the messaged is automatically acknowledged
        return Task.CompletedTask;
    }
}