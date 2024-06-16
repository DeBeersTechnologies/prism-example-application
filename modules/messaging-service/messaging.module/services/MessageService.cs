namespace messaging.services;
public sealed class MessageService : IMessageService
{
    public string GetMessage()
    {
        return "Hello from the Message Service";
    }
}
