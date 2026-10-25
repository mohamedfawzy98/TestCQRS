using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TestCQRS.Services
{
    public class RabbitMQConsumeServices : IHostedService
    {
        IConnection _connection;
        IChannel _channel;
        public RabbitMQConsumeServices()
        {
            var factory = new ConnectionFactory(){HostName = "localhost",};
            _connection = factory.CreateConnectionAsync().Result;  // Connection Server RabbitMQ
            _channel = _connection.CreateChannelAsync().Result;    // Channel Server RabbitMQ  
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Consume Message from Queue

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += Consumer_Receive();

            _channel.BasicConsumeAsync(queue: "Q1VS", autoAck: false, consumer: consumer);

            return Task.CompletedTask;

        }

        private AsyncEventHandler<BasicDeliverEventArgs> Consumer_Receive()
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
