﻿namespace modules.messaging;
public class MessageService : IMessageService
{
    public string GetMessage()
    {
        return "Hello from the Message Service";
    }
}
