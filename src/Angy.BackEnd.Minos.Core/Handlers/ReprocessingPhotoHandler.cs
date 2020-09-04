using System.Threading;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Core.Requests;
using Angy.BackEnd.Minos.Data.Model;
using MediatR;

namespace Angy.BackEnd.Minos.Core.Handlers
{
    public class ReprocessingPhotoHandler : INotificationHandler<ReprocessingPhotoRequest>
    {
        readonly ICopyScaledPhotoProcessor _processor;

        readonly IMinosReadingGateway _readingGateway;
        readonly IMinosWritingGateway _writingGateway;
        readonly IAcheronGateway _acheronGateway;

        public ReprocessingPhotoHandler(ICopyScaledPhotoProcessor processor, IMinosReadingGateway readingGateway, IMinosWritingGateway writingGateway, IAcheronGateway acheronGateway)
        {
            _processor = processor;
            _readingGateway = readingGateway;
            _writingGateway = writingGateway;

            _acheronGateway = acheronGateway;
        }

        public async Task Handle(ReprocessingPhotoRequest notification, CancellationToken cancellationToken)
        {
            var photo = notification.Photo;

            var story = await _readingGateway.GetStoryAsync(photo.Id);

            story.Imported = await _processor.ScaleAsync(story);
            story.Retrieves++;

            if (!story.Imported && story.Retrieves < notification.Retrieves) await OnErrorAsync(photo);

            await UpsertStoriesAsync(story);
        }

        async Task OnErrorAsync(Photo photo) => await _acheronGateway.SendAsync(photo);

        async Task UpsertStoriesAsync(Story story) => await _writingGateway.UpsertStoryAsync(story);
    }
}