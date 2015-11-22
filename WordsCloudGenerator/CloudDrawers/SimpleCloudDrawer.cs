using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordsCloudGenerator.CloudDrawers
{
    class SimpleCloudDrawer : ICloudDrawer
    {
        public Bitmap FormCloud(Bitmap bitmap, Configuration config, List<string> words)
        {
            using (var g = Graphics.FromImage(bitmap))
            {
                var wordHeight = config.Height/words.Count;
                for (var i = 0; i < words.Count; i++)
                {
                    g.DrawString(words[i], new Font(config.Font, Math.Max(config.MaxFontSize-2*i, config.MinFontSize)),
                        new SolidBrush(ColorTranslator.FromHtml(config.Colors[i % config.Colors.Count])),
                        new PointF(0, i * wordHeight));
                }
            }
            return bitmap;
        }
    }
}
