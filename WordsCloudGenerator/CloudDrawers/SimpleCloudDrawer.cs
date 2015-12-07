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
                PointF offset = new PointF(0, 0);
                float maxOffsetForNextColumn = 0;
                graphics.Clear(ColorTranslator.FromHtml(config.BackgroundColor));
                for (var i = 0; i < words.Count; i++)
                {
                    var font = new Font(config.Font, Math.Max(config.MaxFontSize - 2*i, config.MinFontSize));
                    SizeF textSize = graphics.MeasureString(words[i], font);
                    offset = CountOffset(offset, textSize, bitmap.Height, maxOffsetForNextColumn);
                    maxOffsetForNextColumn = (Math.Abs(offset.Y) < 0.1) ? 0 : maxOffsetForNextColumn;
                    if (offset.X >= bitmap.Width)
                        break;
                    graphics.DrawString(words[i], font, 
                        new SolidBrush(ColorTranslator.FromHtml(config.Colors[i % config.Colors.Count])),
                        offset);
                    offset.Y += textSize.Height;
                    maxOffsetForNextColumn = Math.Max(maxOffsetForNextColumn, textSize.Width);
                }
            }
            return bitmap;
        }

        private PointF CountOffset(PointF oldOffset, SizeF textSize, int height, float maxOffset)
        {
            return (oldOffset.Y + textSize.Height > height) ?
                new PointF(oldOffset.X + maxOffset, 0) : oldOffset;
        }
    }
}
