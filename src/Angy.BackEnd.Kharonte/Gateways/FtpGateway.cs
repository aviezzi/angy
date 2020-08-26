using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.BackEnd.Kharonte.Options;
using Angy.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Angy.BackEnd.Kharonte.Gateways
{
    public class FtpGateway : IFtpGateway
    {
        readonly KharonteOptions _options;
        readonly ILogger<KharonteReadingGateway> _logger;

        public FtpGateway(IOptions<KharonteOptions> options, ILogger<KharonteReadingGateway> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public Result<IEnumerable<Photo>, IEnumerable<Model.Error>> RetrievePendingPhotos(IEnumerable<string> accumulatedPaths, int chunk)
        {
            var pathsResult = Result.Try(
                () => Directory.GetFiles(_options.SourceFolder),
                ex => _logger.LogError(ex, $"Cannot get photos from: {_options.SourceFolder} ")
            );

            if (pathsResult.HasError()) return Result<IEnumerable<Model.Error>>.Success(new List<Photo>().AsEnumerable());

            var photos = new List<Photo>();
            var errors = new List<Model.Error>();

            var paths = pathsResult.Success
                .Where(path => !accumulatedPaths.Contains(path))
                .Take(chunk);

            foreach (var path in paths)
            {
                var filenameResult = Result.Try(
                    () => Path.GetFileNameWithoutExtension(path).Trim(),
                    ex => _logger.LogError(ex, $"Cannot retrieve filename for path: {path}")
                );

                var extensionResult = Result.Try(
                    () => Path.GetExtension(path).Trim(),
                    ex => _logger.LogError(ex, $"Cannot retrieve extension for path: {path}")
                );

                if (!extensionResult.HasError() && !filenameResult.HasError())
                    photos.Add(new Photo
                    {
                        Path = path,
                        Filename = filenameResult.Success,
                        Extension = extensionResult.Success
                    });

                if (extensionResult.HasError()) errors.Add(new Model.Error.InvalidExtension());

                if (filenameResult.HasError()) errors.Add(new Model.Error.InvalidFileName());
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Model.Error>>(photos, errors);
        }

        public async Task<Result<IEnumerable<Photo>, IEnumerable<Model.Error>>> CopyPhotosAsync(IEnumerable<Photo> photos)
        {
            var success = new List<Photo>();
            var errors = new List<Model.Error>();

            foreach (var photo in photos)
            {
                var result = await Result.Try(async () =>
                    {
                        await using var sourceStream = new FileStream(photo.Path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
                        await using var destinationStream = new FileStream(Path.Combine(_options.OriginalFolder, $"{photo.Filename}{photo.Extension}"), FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);

                        await sourceStream.CopyToAsync(destinationStream);
                    },
                    ex => _logger.LogError(ex, $"Copy Failed! Id: {photo.Id}, Path: {photo.Path}"));

                if (result.HasError())
                    errors.Add(new Model.Error.CopyFailed());
                else
                    success.Add(photo);
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Model.Error>>(success, errors);
        }

        public Result<IEnumerable<Photo>, IEnumerable<Model.Error>> DeletePhotos(IEnumerable<Photo> photos)
        {
            var success = new List<Photo>();
            var errors = new List<Model.Error>();

            foreach (var photo in photos)
            {
                var result = Result.Try(
                    () => File.Delete(photo.Path),
                    ex => _logger.LogError(ex, $"Delete Failed! {photo.Path}"));

                if (result.HasError())
                    errors.Add(new Model.Error.CopyFailed());
                else
                    success.Add(photo);
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Model.Error>>(success, errors);
        }
    }
}