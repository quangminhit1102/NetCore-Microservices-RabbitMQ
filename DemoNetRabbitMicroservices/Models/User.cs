namespace DemoNetRabbitMicroservices.Models
{
    public class Product
    {
        public int ProductId
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        } = string.Empty;
        public string ProductDescription
        {
            get;
            set;
        } = string.Empty;
        public int ProductPrice
        {
            get;
            set;
        }
        public int ProductStock
        {
            get;
            set;
        }
    }
}