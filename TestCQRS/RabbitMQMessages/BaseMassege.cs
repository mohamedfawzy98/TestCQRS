namespace TestCQRS.RabbitMQMessages
{
    public class BaseMassege
    {
        public DateTime DateSender { get; set; } = DateTime.Now;
        public string Sender { get; set; }
        public string Acion { get; set; }
        public virtual string MassegeType { get; set; }

    }
}
