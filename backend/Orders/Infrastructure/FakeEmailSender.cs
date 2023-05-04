namespace Orders.Infrastructure;

public class FakeEmailSender : IEmailSender
{
    private readonly ILogger<FakeEmailSender> logger;

    public FakeEmailSender(ILogger<FakeEmailSender> logger)
    {
        this.logger = logger;
    }
    
    public Task Send(string address, string message)
    {
        logger.LogInformation("Fake email sent to {Address}. Message: {Message}", address, message);
        
        return Task.CompletedTask;
    }
}