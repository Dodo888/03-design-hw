using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using WordsCloudGenerator.CloudDrawers;

namespace WordsCloudGenerator.Applications
{
    public class ConsoleApplication : IApplicationType
    {
        private readonly Configuration Config;
        public Dictionary<string, ImageFormat> Formats = new Dictionary<string, ImageFormat>
        {
            {"png", ImageFormat.Png},
            {"jpeg", ImageFormat.Jpeg},
            {"bmp", ImageFormat.Bmp}
        };

        public Dictionary<string, ICloudDrawer> Algorithms = new Dictionary<string, ICloudDrawer>
        {
            {"simple", new SimpleCloudDrawer()},
            {"random", new RandomCloudDrawer()}
        }; 

        public ConsoleApplication(Configuration config)
        {
            Config = config;
        }

        public void CreateImage(string imagePath, List<string> words)
        {
            using (var bitmap = new Bitmap(Config.Width, Config.Height))
            {
                ICloudDrawer cloudDrawer = Algorithms[Config.Algorithm];
                var image = cloudDrawer.FormCloud(bitmap, Config, words);
                image.Save(imagePath + "." + Config.ImageFormat, Formats[Config.ImageFormat]);
            }
        }
    }
}
