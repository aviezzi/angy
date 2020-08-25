namespace Angy.BackEnd.Kharonte.Model
{
    public abstract class Error
    {
        public sealed class InvalidFileName : Error
        {
        }

        public sealed class InvalidExtension : Error
        {
        }

        public sealed class CopyFailed : Error
        {
        }

        public sealed class SendFailed : Error
        {
        }
    }
}