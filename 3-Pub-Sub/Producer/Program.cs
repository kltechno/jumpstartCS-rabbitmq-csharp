using System;
using System.Text;
using RabbitMQ.Client;

// 1 Create a connection to RabbitMQ using ConnectionFactory class.
var factory = new ConnectionFactory() { HostName = "localhost" };

using var connection = factory.CreateConnection();

// 2 Create a channel to communicate with RabbitMQ. (English Channel)
using var channel = connection.CreateModel();

// 3 Declare an exchange of type Fanout with name pubsut  (Landing craft exchanges)
channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);

var message = "Hello I want to broadcast this message";

// 4 Convert your msg to byte array
var body = Encoding.UTF8.GetBytes(message);

// 5 Publish to exchange with an empty routing key, and no properties.
channel.BasicPublish(exchange: "pubsub", "", null, body);

Console.WriteLine($"Send message: {message}");