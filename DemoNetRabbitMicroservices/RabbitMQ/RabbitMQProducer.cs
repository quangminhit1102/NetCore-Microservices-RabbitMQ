using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DemoNetRabbitMicroservices.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using var channel = connection.CreateModel();

            // Declare Exchange
            channel.ExchangeDeclare("product-exchange", ExchangeType.Direct, durable: true, autoDelete: false, null);

            // Declare Queue
            channel.QueueDeclare("product-queue", exclusive: false);

            // Bind Queue to an Exchange
            channel.QueueBind(queue: "product-queue", exchange: "product-exchange", routingKey: "product", arguments: null);

            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the product queue
            channel.BasicPublish(exchange: "product-exchange", routingKey: "product", body: body);
        }
    }
}
