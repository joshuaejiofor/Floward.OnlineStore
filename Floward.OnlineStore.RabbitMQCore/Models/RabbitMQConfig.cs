namespace Floward.OnlineStore.RabbitMQCore.Models
{
    public class RabbitMQConfig
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string AutomaticRecoveryEnabled { get; set; }
        public string RequestedHeartbeat { get; set; }

        public string Queue { get; set; }
        public string Exchange { get; set; }
    }
}
