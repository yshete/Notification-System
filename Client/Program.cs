using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Client;
using Client.Observers;

class PusherClient
{
    public static void Main()
    {
        ConcreteSubject s = ConcreteSubject.GetInstance();
        s.Attach(new PusherObserver(s));
        Listen(s);
    }

    private static void Listen(ConcreteSubject concreteSubject)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", Password = "Password@123", UserName = "user" };
        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("notification", "fanout");

                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queueName, "notification", "");
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(queueName, true, consumer);

                Console.WriteLine(" [*] Waiting for notification." +
                                  "To exit press CTRL+C");
                while (true)
                {
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    var body = Encoding.UTF8.GetString(ea.Body);
                    Console.WriteLine(" [x] {0}", body);
                    concreteSubject.SubjectState = body;
                    concreteSubject.Notify();
                }
            }
        }
    }
}

