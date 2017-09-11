using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using RabbitMQ_MassTransit3;

namespace RabbitMQ_MassTransit3
{

    public class SomethingHappenedConsumer
    {
        public void MessageConsumer()
        {
            Log4NetLogger.Use();
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                x.ReceiveEndpoint(host, "Local_Testing", e =>
                  e.Consumer<TestConsumer>());
            });
            var busHandle = bus.Start();
            Console.ReadKey();
            busHandle.Stop();
        }
    }
    

    public class TestConsumer : IConsumer<ISomethingHappened>
    {
        public Task Consume(ConsumeContext<ISomethingHappened> context)
            {
                Console.Write("TXT: " + context.Message.What);
                Console.Write("  SENT: " + context.Message.When);
                Console.Write("  PROCESSED: " + DateTime.Now);
                Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId + ")");

            var routerQueueEndpoint = new Uri("rabbitmq://localhost/");
            return context.Send(routerQueueEndpoint, context.Message);
            //context.Send("rabbitmq://localhost/Local_Publish", context.Message );


            return Task.FromResult(0);
            }
    }
    
}
