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
                float offsetY = 0;
                float offsetX = 0;
                float maxOffsetForNextColumn = 0;
                graphics.Clear(ColorTranslator.FromHtml(config.BackgroundColor));
                for (var i = 0; i < words.Count; i++)
                {
                    var font = new Font(config.Font, Math.Max(config.MaxFontSize - 2*i, config.MinFontSize));
                    SizeF textSize = graphics.MeasureString(words[i], font);
                    if (offsetY + textSize.Height > bitmap.Height)
                    {
                        offsetX = maxOffsetForNextColumn;
                        maxOffsetForNextColumn = 0;
                        offsetY = 0;
                        if (offsetX > bitmap.Width)
                            break;
                    }
                    graphics.DrawString(words[i], font, new SolidBrush(ColorTranslator.FromHtml(config.Colors[i % config.Colors.Count])),
                        new PointF(offsetX, offsetY));
                    offsetY += textSize.Height;
                    maxOffsetForNextColumn = Math.Max(maxOffsetForNextColumn, textSize.Width);
                }
            }
            return bitmap;
        }
    }
}
