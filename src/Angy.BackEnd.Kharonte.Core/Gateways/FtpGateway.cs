using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Kharonte.Core.Gateways
{
    public class FtpGateway : IFtpGateway
    {
        readonly ILogger<KharonteReadingGateway> _logger;

        public FtpGateway(ILogger<KharonteReadingGateway> logger)
        {
            _logger = logger;
        }

        public Result<IEnumerable<Photo>, IEnumerable<Error>> RetrievePendingPhotos(IEnumerable<string> accumulatedPaths, string source, int chunk)
        {
            var pathsResult = Result.Try(
                () => Directory.GetFiles(source),
                ex => _logger.LogError(ex, $"Cannot get photos from: {source}.")
            );

            if (pathsResult.HasError()) return Result<IEnumerable<Error>>.Success(new List<Photo>().AsEnumerable());

            var photos = new List<Photo>();
            var errors = new List<Error>();

            var paths = pathsResult.Success
                .Where(path => !accumulatedPaths.Contains(path))
                .Take(chunk);

            foreach (var path in paths)
            {
                var filenameResult = Result.Try(
                    () => Path.GetFileNameWithoutExtension(path).Trim(),
                    ex => _logger.LogError(ex, $"Cannot retrieve filename for path: {path}.")
                );

                var extensionResult = Result.Try(
                    () => Path.GetExtension(path).Trim(),
                    ex => _logger.LogError(ex, $"Cannot retrieve extension for path: {path}.")
                );

                if (!extensionResult.HasError() && !filenameResult.HasError())
                    photos.Add(new Photo
                    {
                        Path = path,
                        Filename = filenameResult.Success,
                        Extension = extensionResult.Success
                    });

                if (extensionResult.HasError()) errors.Add(new Error.GetExtensionFailed());

                if (filenameResult.HasError()) errors.Add(new Error.GetFilenameFailed());
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Error>>(photos, errors);
        }

        public async Task<Result<IEnumerable<Photo>, IEnumerable<Error>>> CopyPhotosAsync(IEnumerable<Photo> photos, string destination)
        {
            var success = new List<Photo>();
            var errors = new List<Error>();

            foreach (var photo in photos)
            {
                var result = await Result.Try(async () =>
                    {
                        await using var sourceStream = new FileStream(photo.Path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
                        await using var destinationStream = new FileStream(Path.Combine(destination, $"{photo.Filename}{photo.Extension}"), FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);

                        await sourceStream.CopyToAsync(destinationStream);
                    },
                    ex => _logger.LogError(ex, $"Copy Failed! Id: {photo.Id}, Path: {photo.Path}"));

                if (result.HasError())
                    errors.Add(new Error.CopyFailed(photo));
                else
                    success.Add(photo);
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Error>>(success, errors);
        }

        public Result<IEnumerable<Photo>, IEnumerable<Error>> DeletePhotos(IEnumerable<Photo> photos)
        {
            var success = new List<Photo>();
            var errors = new List<Error>();

            foreach (var photo in photos)
            {
                var result = Result.Try(
                    () => File.Delete(photo.Path),
                    ex => _logger.LogError(ex, $"Delete Failed! {photo.Path}"));

                if (result.HasError())
                    errors.Add(new Error.DeleteFailed(photo));
                else
                    success.Add(photo);
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Error>>(success, errors);
        }
    }
}