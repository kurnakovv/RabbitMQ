using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace Consumer.Services
{
    public class RabbitMqListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _model;

        public RabbitMqListener()
        {
            ConnectionFactory connectionFactory = new() { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            EventingBasicConsumer consumer = new(_model);
            consumer.Received += (chanel, eventArguments) =>
            {
                string body = Encoding.UTF8.GetString(eventArguments.Body.ToArray());
                Debug.WriteLine(body); // Any logic here.
                _model.BasicAck(eventArguments.DeliveryTag, false);
            };
            _model.BasicConsume("TestName", false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _model.Close();
            _connection.Close();
            base.Dispose();
        }

    }
}
