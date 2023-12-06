import amqp from "amqplib";

const queue = "product-queue";
const exchange = "product-exchange";

const text = {
  ProductId: 78693443,
  ProductName: "string",
  ProductDescription: "string",
  ProductPrice: 0,
  ProductStock: 0,
};

(async () => {
  let connection;
  try {
    connection = await amqp.connect("amqp://localhost");
    const channel = await connection.createChannel();

    await channel.assertQueue(queue, { durable: false, autoDelete: true });
    await channel.assertExchange(exchange, "direct");
    await channel.bindQueue(queue, exchange, "product");

    // // Send To Queue
    // channel.sendToQueue(queue, Buffer.from(JSON.stringify(text)));

    // // Publish Exchange
    channel.publish(exchange, "product", Buffer.from(JSON.stringify(text)));

    console.log(" [x] Sent '%s'", text);
    await channel.close();
  } catch (err) {
    console.warn(err);
  } finally {
    if (connection) await connection.close();
  }
})();
