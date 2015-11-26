using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsCloudGenerator
{
    public class Configuration
    {
        public int Width;
        public int Height;
        public string Font;
        public List<string> Colors;
        public string ImageFormat;
        public int MinFontSize;
        public int MaxFontSize;
        public string Algorithm;
        public string TextType;
        public int WordsAmount;
        public string BackgroundColor;

        public Configuration()
        {
        }

        public Configuration(string configFile)
        {
            var configs = File.ReadAllLines(configFile);
            var size = configs[0].Split();
            Width = int.Parse(size[0]);
            Height = int.Parse(size[1]);
            Font = configs[1];
            var fontSizes = configs[2].Split();
            MinFontSize = int.Parse(fontSizes[0]);
            MaxFontSize = int.Parse(fontSizes[1]);
            BackgroundColor = configs[3];
            Colors = configs[4].Split().ToList();
            ImageFormat = configs[5];
            Algorithm = configs[6];
            TextType = configs[7];
            WordsAmount = int.Parse(configs[8].Split()[0]);
        }
    }
}
