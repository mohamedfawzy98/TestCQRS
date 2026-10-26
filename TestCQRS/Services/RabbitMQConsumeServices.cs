using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TestCQRS.Dtos;
using TestCQRS.RabbitMQMessages;

namespace TestCQRS.Services
{
    public class RabbitMQConsumeServices : IHostedService
    {
        IConnection _connection;
        IChannel _channel;
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMQConsumeServices(IMediator mediator , IServiceScopeFactory scopeFactory)
        {
            var factory = new ConnectionFactory(){HostName = "localhost",};
            _connection = factory.CreateConnectionAsync().Result;  // Connection Server RabbitMQ
            _channel = _connection.CreateChannelAsync().Result;    // Channel Server RabbitMQ  
            _mediator = mediator;
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Consume Message from Queue

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += Consumer_Receive;

            _channel.BasicConsumeAsync(queue: "Q1VS", autoAck: false, consumer: consumer);

            return Task.CompletedTask;

        }

        private Task Consumer_Receive(object? sender, BasicDeliverEventArgs e)
        {
            try
            {
                // Read Message from Queue
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Console.WriteLine($"Message Received: {message}");

                var baseMassege = GetBaseMessage(message);

                // CONSUMER
                var consumer =  InvokeConsume(baseMassege);

                // Acknowledge Message
                //   _channel.BasicAckAsync(e.DeliveryTag, multiple: false);
            }
            catch (Exception)
            {
                // Requeue depand on Businuse
                _channel.BasicRejectAsync(e.DeliveryTag, requeue: false);
            }
            return Task.CompletedTask;
        }

        private async Task InvokeConsume(BaseMassege baseMassege)
        {
            // 1 - Get Type
            var namespac = "TestCQRS.RabbitMQConsume";

            var ComsumeType = Type.GetType($"{namespac}.{baseMassege.MassegeType},TestCQRS");
            if(ComsumeType == null)
                throw new Exception($"Type {baseMassege.MassegeType} not found in namespace {namespac}");

            // 2 - Create Instance of Type
            var consumeInstance = Activator.CreateInstance(ComsumeType, _mediator);

            var method = ComsumeType.GetMethod("Consume");

            method?.Invoke(consumeInstance, new object[] { baseMassege });
        }

        private BaseMassege GetBaseMessage(string message)
        {
            // Get Type of Message from Massege By Newtonsoft
            var jsonobj = Newtonsoft.Json.Linq.JObject.Parse(message);
            var typebasemassege = jsonobj["MassegeType"]?.ToString();

            // All Message Type in Namespace TestCQRS.RabbitMQMessages
            var namespac = "TestCQRS.RabbitMQMessages";

            // USE  REFLCATION TO GET TYPE OF MESSAGE
            Type type = Type.GetType($"{namespac}.{typebasemassege},TestCQRS");

            if (type == null) 
                throw new Exception($"Type {typebasemassege} not found in namespace {namespac}");

            // Now have object from AddCourseMassege 
            var msg = Newtonsoft.Json.JsonConvert.DeserializeObject(message, type) as BaseMassege;

            // To Change Message Type 
            msg.MassegeType = typebasemassege.Replace("Message", "Consume");

            return msg;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

      
    }
}
