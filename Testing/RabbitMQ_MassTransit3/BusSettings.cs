namespace RabbitMQ_MassTransit3
{
    public class BusSettings
    {
        public string ConcurrentConsumerLimit { get; set; }
        public string RetryLimit { get; set; }
        public string IncomingUriString { get; set; }
        public string IncomingQueue { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ushort HeartBeatInSeconds { get; set; }
       
        public OutgoingBusSettings OutgoingBusSettings { get; set; }
    
        public static readonly string ConsumerLimitElementName = "ConcurrentConsumerLimit";
        public static readonly string RetryLimitElementName = "RetryLimit";
        public static readonly string IncomingUriStringElementName = "IncomingUriString";
        public static readonly string IncomingQueueElementName = "IncomingQueue";
        public static readonly string UsernameElementName = "Username";
        public static readonly string PasswordElementName = "Password";
        public static readonly string HeartBeatInSecondsElementName = "HeartBeatInSeconds";
    }

    public class OutgoingBusSettings
    {
        public string BaseUriString { get; set; }
        public string AdtCommandReadyQueue { get; set; }
        public string AdtCommandCompleteQueue { get; set; }
        public string OutgoingQueue { get; set; }
         public ushort InitialQueueCount { get; set; }
        public ushort IncrementQueueCount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConcurrentConsumerLimit { get; set; }
        public string RetryLimit { get; set; }
        public ushort HeartBeatInSeconds { get; set; }

        public static readonly string ConsumerLimitElementName = "ConcurrentConsumerLimit";
        public static readonly string RetryLimitElementName = "RetryLimit";
        public static readonly string BaseUriStringElementName = "BaseUriString";
        public static readonly string AdtCommandReadyQueueElementName = "AdtCommandReadyQueue";
        public static readonly string AdtCommandCompleteQueueElementName = "AdtCommandCompleteQueue";
        public static readonly string OutgoingQueueElementName = "OutgoingQueue";
        public static readonly string InitialQueueCountElementName = "InitialQueueCount";
        public static readonly string IncrementQueueCountElementName = "IncrementQueueCount";
        public static readonly string UsernameElementName = "Username";
        public static readonly string PasswordElementName = "Password";
        public static readonly string HeartBeatInSecondsElementName = "HeartBeatInSeconds";
    }
}
