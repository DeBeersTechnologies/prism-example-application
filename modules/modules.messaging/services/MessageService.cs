namespace modules.messaging.services;
public class MessageService : IMessageService
{
    public string GetMessage()
    {
        return "Hello from the Message Service";
    }
}
