using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// 1 Create a connection to RabbitMQ using ConnectionFactory class.
var factory = new ConnectionFactory() { HostName = "localhost" };

using var connection = factory.CreateConnection();

// 2 Create a channel to communicate with RabbitMQ. (English Channel)
using var channel = connection.CreateModel();

// 3 Declare an exchange of type Fanout with name pubsut  (Landing craft exchanges)
channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);

// 4 Declare a queue and binds to pubsub with an empty routing key. (Queues of food can)
var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: "pubsub", routingKey: "");

// 5 Create a consumer and setup the event handler to convert message body to a string. (Consumers on beach)
var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"FirstConsumer - Recieved new message: {message}");
};

// 6 Start consuming messages from the queue with auto acknowledgement enabled. (Consumers running towards queues of cans)
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine("Consuming");

Console.ReadKey();