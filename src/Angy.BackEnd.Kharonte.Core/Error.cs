using Angy.BackEnd.Kharonte.Data.Model;

namespace Angy.BackEnd.Kharonte.Core
{
    public abstract class Error
    {
        public Photo Photo { get; }

        protected Error() : this(new Photo())
        {
        }

        Error(Photo photo)
        {
            Photo = photo;
        }

        public sealed class InvalidFileName : Error
        {
            public InvalidFileName(Photo photo) : base(photo)
            {
            }
        }

        public sealed class InvalidExtension : Error
        {
            public InvalidExtension(Photo photo) : base(photo)
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
            public CopyFailed(Photo photo) : base(photo)
            {
            }
        }

        public sealed class SendFailed : Error
        {
            public SendFailed(Photo photo) : base(photo)
            {
            }
        }

        public sealed class DeleteFailed : Error
        {
            public DeleteFailed(Photo photo) : base(photo)
            {
            }
        }
    }
}