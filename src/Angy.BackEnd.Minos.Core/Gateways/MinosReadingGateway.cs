using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Data;
using Angy.BackEnd.Minos.Data.Model;
using Angy.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Minos.Core.Gateways
{
    public class MinosReadingGateway : IMinosReadingGateway
    {
        readonly MinosContext _minos;
        readonly ILogger<MinosReadingGateway> _logger;

        public MinosReadingGateway(MinosContext minos, ILogger<MinosReadingGateway> logger)
        {
            _minos = minos;
            _logger = logger;
        }

        public async Task<IEnumerable<Story>> GetStoriesForPhoto(Photo photo)
        {
            var result = await Result.Try(
                async () => (await _minos.Settings.ToListAsync()).AsEnumerable(),
                ex => _logger.LogError(ex, "Cannot read builders settings!"));

            return result.HasError()
                ? new List<Story>()
                : result.Success.Select(setting => new Story
                {
                    SettingId = setting.Id,
                    Setting = setting,
                    PhotoId = photo.Id,
                    Photo = photo,
                    Retrieves = 0,
                    Imported = false
                });
        }

        public async Task<Story> GetStoryAsync(Guid id)
        {
            var result = await Result.Try(async () => await _minos.Stories.Include(story => story.Setting).SingleAsync(story => story.SettingId == id),
                ex => _logger.LogError(ex, $"Cannot get story! Id: {id}"));

            return result.Success;
        }
    }
}