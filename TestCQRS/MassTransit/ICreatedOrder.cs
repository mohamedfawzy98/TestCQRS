namespace TestCQRS.MassTransit
{
    public interface ICreatedOrder
    {
        public int Id { get; }
        public string Address { get; }
    }
}
