using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordsCloudGenerator.CloudDrawers
{
    class RandomCloudDrawer : ICloudDrawer
    {
        public Bitmap FormCloud(Bitmap bitmap, Configuration config, List<string> words)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var random = new Random();
                foreach (var word in words)
                {
                    var font = new Font(config.Font, random.Next(config.MinFontSize, config.MaxFontSize));
                    SizeF textSize = graphics.MeasureString(word, font);
                    graphics.DrawString(word, font, new SolidBrush(ColorTranslator.FromHtml(config.Colors[random.Next(config.Colors.Count)])),
                        new PointF(random.Next(config.Height - (int)textSize.Height), random.Next(config.Width - (int)textSize.Width)));
                }
            }
            return bitmap;
        }
    }
}
