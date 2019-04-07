using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Entity;
using System.Collections.Generic;

class Server
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", Password="Password@123", UserName="user", Port = Protocols.DefaultProtocol.DefaultPort };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare("notification", "fanout");
            IList<long> recipients = new List<long>();
            recipients.Add(1);
            recipients.Add(2);
            var message = new Message { MessageType = "Notify", Payload = new Notification(GetMessageCode(args), recipients).ToString() };
            var body = Encoding.UTF8.GetBytes(message.ToString());
            channel.BasicPublish("notification", "", null, body);
            Console.WriteLine(" [x] Sent {0}", message.ToString());
        }
    }

    private static string GetMessageCode(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Booked");
    }
}