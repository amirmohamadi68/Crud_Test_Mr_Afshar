using Mc2.CrudTest.Application.Interfaces.messaging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrustructure
{
    public class RabbitMqMessagePublisher : IMessagePublisher, IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;
        private const string QueueName = "CustomerEvents";
        public RabbitMqMessagePublisher()
        {
            _connectionFactory = new ConnectionFactory  // i willl use Interface factory at the end
            {
                HostName = "localhost", // RabbitMQ server host
                UserName = "guest",    // default username
                Password = "guest"     // default password
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        public async Task publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

           _channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
            Console.WriteLine($"[x] Sent '{message}'"); // if i  had time i will loging Elastic Search at the end
        }
        public void Dispose()
        {
            // i will impilement dispose method and clean GC at the end
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
