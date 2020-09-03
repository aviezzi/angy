using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Data;
using Angy.BackEnd.Minos.Data.Model;
using Angy.Model;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Minos.Core.Gateways
{
    public class MinosWritingGateway : IMinosWritingGateway
    {
        readonly MinosContext _minos;
        readonly ILogger<IMinosWritingGateway> _logger;

        public MinosWritingGateway(MinosContext minos, ILogger<IMinosWritingGateway> logger)
        {
            _minos = minos;
            _logger = logger;
        }

        public async Task UpsertStoryAsync(Story story)
        {
            _minos.Upsert(story);

            await Result.Try(
                async () => await _minos.SaveChangesAsync(),
                ex =>
                {
                    var message = $"Filename: {story.Photo.Filename}{story.Photo.Shot}{story.Photo.Extension}. Settings: {story.SettingId}. Retrieves: {story.Retrieves}. Imported: {story.Imported}";
                    _logger.LogError(ex, $"Cannot save story: {message}");
                });
        }

        public async Task UpsertStoriesAsync(IEnumerable<Story> stories)
        {
            var enumerable = stories.ToImmutableList();

            foreach (var story in enumerable)
            {
                _minos.Upsert(story);
            }

            await Result.Try(
                async () => await _minos.SaveChangesAsync(),
                ex =>
                {
                    var photo = enumerable.FirstOrDefault()?.Photo;
                    _logger.LogError(ex, $"Cannot save story: {photo?.Filename}{photo?.Shot}{photo?.Extension}");
                });
        }
    }
}