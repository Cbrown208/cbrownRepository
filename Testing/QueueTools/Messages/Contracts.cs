using System;

namespace QueueTools.Messages
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
