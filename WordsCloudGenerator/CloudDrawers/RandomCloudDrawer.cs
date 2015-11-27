using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordsCloudGenerator.CloudDrawers
{
    public class RandomCloudDrawer : ICloudDrawer
    {
        private const int MaxTriesAmount = 100;

        public Bitmap FormCloud(Bitmap bitmap, Configuration config, List<string> words)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var random = new Random();
                var fontSizeDecrease = 0;
                var occupiedAreas = new HashSet<RectangleF>();
                graphics.Clear(ColorTranslator.FromHtml(config.BackgroundColor));
                foreach (var word in words)
                {
                    var font = new Font(config.Font, Math.Max(config.MaxFontSize - 3*fontSizeDecrease, config.MinFontSize));
                    SizeF textSize = graphics.MeasureString(word, font);
                    Point textPlace = DefineArea(occupiedAreas, textSize, new Point(config.Width, config.Height));
                    graphics.DrawString(word, font,
                        new SolidBrush(ColorTranslator.FromHtml(config.Colors[random.Next(config.Colors.Count)])),
                        textPlace);
                    occupiedAreas.Add(new RectangleF(textPlace, textSize));
                    fontSizeDecrease++;
                }
            }
            return bitmap;
        }

        public Point DefineArea(HashSet<RectangleF> occupiedAreas, SizeF textSize, Point bitmap)
        {
            var random = new Random();
            var hasStartPoint = false;
            var width = 0;
            var height = 0;
            var triesAmount = 0;
            while (!hasStartPoint || HasIntersection(occupiedAreas, width, height, textSize))
            {
                if (triesAmount > MaxTriesAmount)
                    break;
                width = random.Next(bitmap.X - (int)textSize.Width);
                height = random.Next(bitmap.Y - (int)textSize.Height);
                triesAmount++;
                hasStartPoint = true;
            }
            return new Point(width, height);
        }

        public bool HasIntersection(HashSet<RectangleF> occupiedAreas, int width, int height, SizeF textSize)
        {
            var anotherArea = new RectangleF(new PointF(width, height), textSize);
            return occupiedAreas.Any(area => area.IntersectsWith(anotherArea));
        }
    }
}
