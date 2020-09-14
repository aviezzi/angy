namespace Angy.BackEnd.Minos
{
    public class MinosOptions
    {
        public string SourceDirectory { get; set; }
        public string RetouchedDirectory { get; set; }
        public int Retrieves { get; set; }
        public int Quality { get; set; }
        
        public KafkaOptions KafkaOptions { get; set; }
    }

    public class KafkaOptions
    {
        public string BootServers { get; set; }
        public string TopicNew { get; set; }
        public string TopicReprocessing { get; set; }

        public int MessageRetryCount { get; set; }
        public int MessageAttempt { get; set; }
    }
}