using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Angy.BackEnd.Minos.Core.Builders
{
    public struct PhotoBuilder
    {
        readonly int _height;
        readonly int _width;
        readonly int _quality;

        public PhotoBuilder(int height, int width, int quality)
        {
            _height = height;
            _width = width;
            _quality = quality;
        }

        public bool TryBuild(string path, Stream output)
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