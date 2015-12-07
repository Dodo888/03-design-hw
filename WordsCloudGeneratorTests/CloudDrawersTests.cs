using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordsCloudGenerator.CloudDrawers;

namespace WordsCloudGeneratorTests
{
    [TestClass]
    public class CloudDrawersTests
    {
        [TestMethod]
        public void Should_SelectAvailablePoint_NotAtTheStart()
        {
            var cloudDrawer = new RandomCloudDrawer();
            var occupiedArea = new RectangleF(0, 0, 10, 10);
            var textSize = new SizeF(10, 10);
            var resultPoint = cloudDrawer.DefineArea(new HashSet<RectangleF> { occupiedArea },
                textSize,
                new Point(30, 30));
            Assert.IsTrue(!HasIntersection(occupiedArea, resultPoint, textSize));
        }

        [TestMethod]
        public void Should_SelectAvailablePoint_NotAtTheMiddle()
        {
            var cloudDrawer = new RandomCloudDrawer();
            var occupiedArea = new RectangleF(10, 10, 20, 20);
            var textSize = new SizeF(10, 10);
            var resultPoint = cloudDrawer.DefineArea(new HashSet<RectangleF> { occupiedArea },
                textSize,
                new Point(30, 30));
            Assert.IsTrue(!HasIntersection(occupiedArea, resultPoint, textSize));
        }

        [TestMethod]
        public void Should_SelectAvailablePoint_NotAtTheEnd()
        {
            var cloudDrawer = new RandomCloudDrawer();
            var occupiedArea = new RectangleF(20, 20, 30, 30);
            var textSize = new SizeF(10, 10);
            var resultPoint = cloudDrawer.DefineArea(new HashSet<RectangleF> { occupiedArea },
                textSize,
                new Point(30, 30));
            Assert.IsTrue(!HasIntersection(occupiedArea, resultPoint, textSize));
        }

        public bool HasIntersection(RectangleF occupiedArea, Point offset, SizeF textSize)
        {
            var anotherArea = new RectangleF(offset, textSize);
            return occupiedArea.IntersectsWith(anotherArea);
        }
    }
}
