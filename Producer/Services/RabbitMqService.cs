using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Producer.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(string message)
        {
            ConnectionFactory connectionFactory = new() { HostName = "localhost" };
            using IConnection connection = connectionFactory.CreateConnection();
            using IModel model = connection.CreateModel();
            model.QueueDeclare(
                queue: "TestName",
                durable: false,
                exclusive: false,
                autoDelete: false);
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            model.BasicPublish(
                exchange: string.Empty,
                routingKey: "TestName",
                body: bytes);
        }

        public void SendMessage(object obj)
        {
            string message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }
    }
}
