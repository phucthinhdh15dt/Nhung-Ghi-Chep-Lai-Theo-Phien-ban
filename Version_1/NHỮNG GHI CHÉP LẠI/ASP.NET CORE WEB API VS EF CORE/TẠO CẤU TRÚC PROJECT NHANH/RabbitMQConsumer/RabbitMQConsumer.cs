using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testdbfirst.Models;
using testdbfirst.RabbitQM;

namespace testdbfirst.RabbitMQKhachHang
{
    public class RabbitMQConsumer
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;

        private const string ExchangeName = "exchangeName_demo_RabbitMQ";
        private const string CustomerQueueName = "customer";

        public void CreateConnection()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Close()
        {
            _connection.Close();
        }

        public IEnumerable<Customer> ProcessMessages()
        {
            List<Customer> list = new List<Customer>();
            using (_connection = _factory.CreateConnection())
            {
                using (var channel = _connection.CreateModel())
                {


                    channel.ExchangeDeclare(ExchangeName, "topic");
                    channel.QueueDeclare(CustomerQueueName,
                        true, false, false, null);

                    channel.QueueBind(CustomerQueueName, ExchangeName,
                        "product.customer");

                    channel.BasicQos(0, 10, false);
                    Subscription subscription = new Subscription(channel,
                        CustomerQueueName, false);

                    while (true)
                    {

                        BasicDeliverEventArgs deliveryArguments = subscription.Next();
                        if (deliveryArguments == null)
                        {
                            subscription.Close();
                            
                            _connection.Close();
                            break ;
                        }
                        else
                        {
                            var message =
                                (Customer)deliveryArguments.Body.DeSerialize(typeof(Customer));
                            list.Add(message);
                        }
                    }


                }
            }

            return list;
        }


    }
}

