using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Angy.BackEnd.Minos.Core.Abstract;
using Angy.BackEnd.Minos.Core.Handlers;
using Angy.BackEnd.Minos.Data.Model;
using Microsoft.Extensions.Logging;

namespace Angy.BackEnd.Minos.Core.Processors
{
    public class CopyScaledPhotoProcessor : ICopyScaledPhotoProcessor
    {
        readonly string _source;
        readonly string _destination;
        readonly int _quality;

        readonly ILogger<CopyScaledPhotoProcessor> _logger;

        public CopyScaledPhotoProcessor(string source, string destination, int quality, ILogger<CopyScaledPhotoProcessor> logger)
        {
            _source = source;
            _destination = destination;
            _quality = quality;

            _logger = logger;
        }

        public async Task<bool> ScaleAsync(Story story)
        {
            var builder = new PhotoScaler(story.Setting.Height, story.Setting.Width, _quality);

            _logger.LogInformation($"Instantiate builder: {story.SettingId} at {DateTime.Now.ToUniversalTime()}.");

            var filename = $"{story.Photo.Filename}{story.Photo.Shot}{story.Photo.Extension}";

            var source = Path.Combine(_source, filename);
            var destination = Path.Combine(_destination, filename);

            _logger.LogInformation($"Parameters extracted: filename: {filename}, source: {source}, destination: {destination}; at {DateTime.Now.ToUniversalTime()}.");

            await using var output = File.Open(destination, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
            var imported = builder.TryScale(source, output);

            _logger.LogInformation($"Photo imported: {imported} at {DateTime.Now.ToUniversalTime()}.");

            return imported;
        }

        readonly struct PhotoScaler
        {
            readonly int _height;
            readonly int _width;
            readonly int _quality;

            public PhotoScaler(int height, int width, int quality)
            {
                _height = height;
                _width = width;
                _quality = quality;
            }

            public bool TryScale(string path, Stream output)
            {
                try
                {
                    using var image = new Bitmap(Image.FromFile(path));

                    var resized = new Bitmap(_width, _height);

                    using var graphics = Graphics.FromImage(resized);

                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, x: 0, y: 0, _width, _height);

                    var codec = ImageCodecInfo.GetImageDecoders().First(info => info.FormatID == ImageFormat.Jpeg.Guid);
                    var encoderParameters = new EncoderParameters(count: 1) { Param = { [0] = new EncoderParameter(Encoder.Quality, _quality) } };

                    resized.Save(output, codec, encoderParameters);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}