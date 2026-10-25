using RabbitMQ.Client;
using System.Text;

namespace TestCQRS.Services
{
    public class RabbitMQServices
    {
        IConnection _connection;
        IChannel _channel;
        public RabbitMQServices()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
            };
            _connection = factory.CreateConnectionAsync().Result;  // Connection Server RabbitMQ
            _channel = _connection.CreateChannelAsync().Result;    // Channel Server RabbitMQ

            // Create Exchange
            _channel.ExchangeDeclareAsync(exchange: "Ex1VS", type: ExchangeType.Direct);

            // Create Queue
            _channel.QueueDeclareAsync(queue: "Q1VS", durable: true, exclusive: false, autoDelete: false);

            // Bind Queue to Exchange
            _channel.QueueBindAsync(queue: "Q1VS", exchange: "Ex1VS", routingKey: "key1VS");
        }

        public void PublishMessage(string Message)
        {
            var body = Encoding.UTF8.GetBytes(Message);
          //  _channel.BasicPublishAsync("Ex1", "key1", body: body);
            _channel.BasicPublishAsync("Ex1VS", "key1VS", body: body);
        }
    }
}
