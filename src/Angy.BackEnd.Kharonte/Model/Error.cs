namespace Angy.BackEnd.Kharonte.Model
{
    public abstract class Error
    {
        public string Filename { get; }
        public string Extension { get; }

        protected Error() : this(string.Empty, string.Empty)
        {
        }

        Error(string filename, string extension)
        {
            Filename = filename;
            Extension = extension;
        }

        public sealed class InvalidFileName : Error
        {
            public InvalidFileName(string filename, string extension) : base(filename, extension)
            {
            }
        }

        public sealed class InvalidExtension : Error
        {
            public InvalidExtension(string filename, string extension) : base(filename, extension)
            {
            }
        }

        public sealed class GetFilenameFailed : Error
        {
        }

        public sealed class GetExtensionFailed : Error
        {
        }

        public sealed class CopyFailed : Error
        {
            public CopyFailed(string filename, string extension) : base(filename, extension)
            {
            }
        }

        public sealed class SendFailed : Error
        {
            public SendFailed(string filename, string extension) : base(filename, extension)
            {
            }
        }

        public sealed class DeleteFailed : Error
        {
            public DeleteFailed(string filename, string extension) : base(filename, extension)
            {
            }
        }
    }
}