namespace Orders.Infrastructure;

public interface IEmailSender
{
    Task Send(string address, string message);
}