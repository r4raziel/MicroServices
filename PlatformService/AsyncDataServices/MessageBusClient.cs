using System.Text;
using System.Text.Json;
using AutoMapper;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{

    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMqPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                //subscribe event
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("---> Conneted to MessageBus");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"---> Could not connect to Message Bus: {ex.Message}");
            }

        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("----> RabbitMQ Connection open, sending message....");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("----> RabbitMQ connection is closed, not sending..");
            }

        }
        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                                    exchange: "trigger",
                                    routingKey: "",
                                    basicProperties: null,
                                    body: body
                                );
            Console.WriteLine($"---> We have sent:{message}");

        }
        public void Dispose()
        {

            Console.WriteLine("---> Message Bus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();

            }
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("---> RabbitMQ Connection ShutDown");

        }
    }
}