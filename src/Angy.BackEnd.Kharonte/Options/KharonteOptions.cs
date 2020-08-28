namespace Angy.BackEnd.Kharonte.Options
{
    public class KharonteOptions
    {
        public string SourceDirectory { get; set; }
        public string OriginalDirectory { get; set; }
        public string ErrorsDirectory { get; set; }
        public int PhotoChunk { get; set; }
        public int OlderThan { get; set; }
    }
}