using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordsCloudGenerator.CloudDrawers
{
    public class SimpleCloudDrawer : ICloudDrawer
    {
        public Bitmap FormCloud(Bitmap bitmap, Configuration config, List<string> words)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                float height = 0;
                float width = 0;
                float maxWidth = 0;
                graphics.Clear(ColorTranslator.FromHtml(config.BackgroundColor));
                for (var i = 0; i < words.Count; i++)
                {
                    var font = new Font(config.Font, Math.Max(config.MaxFontSize - 2*i, config.MinFontSize));
                    SizeF textSize = graphics.MeasureString(words[i], font);
                    if (height + textSize.Height > bitmap.Height)
                    {
                        width = maxWidth;
                        maxWidth = 0;
                        height = 0;
                        if (width > bitmap.Width)
                            break;
                    }
                    graphics.DrawString(words[i], font, new SolidBrush(ColorTranslator.FromHtml(config.Colors[i % config.Colors.Count])),
                        new PointF(width, height));
                    height += textSize.Height;
                    maxWidth = Math.Max(maxWidth, textSize.Width);
                }
            }
            return bitmap;
        }
    }
}
