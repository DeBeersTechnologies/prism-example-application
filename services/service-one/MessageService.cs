using prism_application.services.service_one.interfaces;

namespace prism_application.services.service_one
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
