using System.Collections.Generic;

namespace Angy.BackEnd.Kharonte.Options
{
    public class KharonteOptions
    {
        public string SourceFolder { get; set; }
        public string OriginalFolder { get; set; }
        public IEnumerable<string> SupportedExtensions { get; set; }
        public int PhotoChunk { get; set; }
        public int OlderThan { get; set; }

        public KafkaOptions Kafka { get; set; }
    }
}