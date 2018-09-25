using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using testRPC.Model;

namespace testRPC
{
    public class Program
    {


        public static void Main(string[] args)
        {
            //Subscribe();
            //Publish();
            //Receive();
            //Send();
            Response();
            Request();
        }
        static void Publish()
        {

            var rand = new Random();
            var bus = RabbitHutch.CreateBus("host=localhost");
            while (true)
            {
                var message = new MyMessage
                    {
                    Name = "thinh",
                    ShoeSize = rand.Next(1, 20)
                    };
                bus.Publish(message);
                Console.WriteLine("san xuat tin nhan");
                Thread.Sleep(1000);

            }
        }
        static void Subscribe()
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            bus.Subscribe<MyMessage>("sub_id", x =>
             {
                 Console.WriteLine("Received message : {0},{1}", x.Name, x.ShoeSize);
             }
            );
        }
        static void Send()
        {
            var rand = new Random();
            var bus = RabbitHutch.CreateBus("host=localhost");
            while (true)
            {
                var myMessage = new MyMessage
                {
                    Name = "thinh",
                    ShoeSize = rand.Next(1, 20)
                };
                var myOtherMessage = new MyOtherMessage()
                {
                    Address = "phuc " + rand.Next(1, 20),
                    Taxes = Convert.ToDecimal(rand.NextDouble())
                };

                bus.Send("phucthinh.queue", myMessage);
                bus.Send("phucthinh.queue", myOtherMessage);
                Console.WriteLine("send 2 tin nhan");
                Thread.Sleep(1000);

            }

        }
        static void Receive()
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            bus.Receive("phucthinh.queue", x => x
            .Add<MyMessage>(m =>
            {
                Console.WriteLine("Received message : {0},{1}", m.Name, m.ShoeSize);
            })
            .Add<MyOtherMessage>(m =>
            {
                Console.WriteLine("Received myOtherMessage : {0},{1:-.0}", m.Address, m.Taxes);
            })
            );
        }
        static void Request()
        {

            var rand = new Random();
            var bus = RabbitHutch.CreateBus("host=localhost");
            var message1 = new MyMessage
            {
                Name = "thinh",
                ShoeSize = 1
            };
            while (true)
            {
                var message = new MyMessage
                {
                    Name = "thinh",
                    ShoeSize = rand.Next(1, 5)
                };
                Console.WriteLine("gui tin nhan");
                var reponse = bus.Request<MyMessage, MyResponse>(message);
                bus.Publish(message);
                
                Console.WriteLine(reponse.Message+"ok");
                if (message.Name.Equals("thinh") && message.ShoeSize==1)
                {
                    Console.WriteLine("break");
                    bus.Dispose();
                    break;
                }
                Thread.Sleep(1000);

            }
        }
        static void Response()
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            bus.Respond<MyMessage,MyResponse>( x =>
            {
                return new MyResponse
                {
                    Message = string.Format("tra ve: {0},{1}", x.Name, x.ShoeSize)
                };
            }
            );
        }
    }
}
