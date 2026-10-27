using MassTransit;

namespace TestCQRS.MassTransit
{
    public class OrderCreatedConsumer : IConsumer<ICreatedOrder>
    {
        public Task Consume(ConsumeContext<ICreatedOrder> context)
        {
            var message = context.Message;
            Console.WriteLine($"Order Created: Id={message.Id}, Address={message.Address}");
            return Task.CompletedTask;
        }
    }
}
