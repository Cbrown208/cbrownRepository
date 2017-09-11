using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MassTransit3
{
    public interface ISomethingHappened
    {
        string What { get; }
        DateTime When { get; }
    }

    class SomethingHappenedMessage : ISomethingHappened
    {
        public string What { get; set; }
        public DateTime When { get; set; }
    }
}
