using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Core.Requests;
using Angy.BackEnd.Minos.Data.Model;
using MediatR;

namespace Angy.BackEnd.Minos.Core.Handlers
{
    public class NewPhotoHandler : INotificationHandler<NewPhotoRequest>
    {
        readonly IMinosReadingGateway _readingGateway;
        readonly IMinosWritingGateway _writingGateway;
        readonly IAcheronGateway _acheronGateway;

        public NewPhotoHandler(IMinosReadingGateway readingGateway, IMinosWritingGateway writingGateway, IAcheronGateway acheronGateway)
        {
            _readingGateway = readingGateway;
            _writingGateway = writingGateway;

            _acheronGateway = acheronGateway;
        }

        public async Task Handle(NewPhotoRequest notification, CancellationToken cancellationToken)
        {
            var photo = notification.Photo;

            var stories = await GetStoriesAsync(photo);

            await UpsertStoriesAsync(stories);

            var success = new List<Story>();

            foreach (var story in stories)
            {
                story.Imported = await Selector(story, photo);

                if (story.Imported)
                    success.Add(story);
                else
                    await OnErrorAsync(photo);
            }

            await UpsertStoriesAsync(success);
        }

        async Task<ImmutableList<Story>> GetStoriesAsync(Photo photo)
        {
            var stories = (await _readingGateway.GetStoriesForPhoto(photo)).ToImmutableList();

            if (!stories.Any()) await OnErrorAsync(photo);

            return stories;
        }

        async Task UpsertStoriesAsync(IEnumerable<Story> stories) => await _writingGateway.UpsertStoriesAsync(stories);

        async Task OnErrorAsync(Photo photo) => await _acheronGateway.SendAsync(photo);
    }
}