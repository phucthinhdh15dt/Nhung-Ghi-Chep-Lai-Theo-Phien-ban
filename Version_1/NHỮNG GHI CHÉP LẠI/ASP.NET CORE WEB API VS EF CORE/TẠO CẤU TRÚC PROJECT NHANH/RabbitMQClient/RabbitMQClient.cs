using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testdbfirst.Models;

namespace testdbfirst.RabbitQM
{
    public class RabbitMQClient
    {
        private static ConnectionFactory _connectionFactory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "exchangeName_demo_RabbitMQ";
        private const string Customer = "customer";

        public void CreateConnection()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            //khoi tao 1 exchangename topic
            _model.ExchangeDeclare(ExchangeName, "topic");
            //tao mot mess queue cardPaymentQueueName
            _model.QueueDeclare(Customer, true, false, false, null);




            //tao mot bind CardPaymentQueueName
            _model.QueueBind(Customer, ExchangeName, "product.customer");

        }
            public void Close()
            {
                _connection.Close();
            }

        public void SendCustomer(Customer customer)
        {
            SendMessage(customer.Serialize(), "product.customer");
        }

        private void SendMessage(byte[] message, string routingKey)
        {
            _model.BasicPublish(ExchangeName, routingKey, null, message);
        }



    }
}
