using TestCQRS.RabbitMQMessages;

namespace TestCQRS.RabbitMQConsume
{
    public interface IConsume<T> where T : BaseMassege
    {
        Task Consume(T message);
    }
}
