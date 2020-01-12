using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;  
namespace ConsumidorDEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            String cola = Convert.ToString(args[0]);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {

                channel.QueueDeclare(cola,false,false,false,null);
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(cola, autoAck: true, consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

        }
    }
}
