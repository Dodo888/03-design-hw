using FakeItEasy;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordsCloudGenerator;
using WordsCloudGenerator.Applications;
using WordsCloudGenerator.FileParsers;
using WordsCloudGenerator.CloudDrawers;

namespace WordsCloudGeneratorTests
{
    [TestClass]
    public class TopWordsTests
    {
        public List<string> TestGetTopWords(Dictionary<string, int> words, Dictionary<string, int> banned, int amount)
        {
            var args = A.Fake<CommandLineArgs>();
            var config = A.Fake<Configuration>();
            config.WordsAmount = amount;
            var fileParser = A.Fake<IFileParser>();
            var application = A.Fake<IApplication>();
            var program = new Program(args, config, fileParser, application);
            return program.GetTopWords(words, banned);
        }

        [TestMethod]
        public void BannedWords_shouldBeRemoved()
        {
            var words = new Dictionary<string, int>
            {
                {"one", 1},
                {"two", 2},
                {"three", 3}
            };
            var bannedWords = new Dictionary<string, int>
            {
                {"one", 5}
            };
            var result = new List<string>{"three", "two"};
            CollectionAssert.AreEqual(TestGetTopWords(words, bannedWords, 10), result);
        }

        [TestMethod]
        public void RedundantWords_shouldBeRemoved()
        {
            var words = new Dictionary<string, int>
            {
                {"one", 1},
                {"two", 2},
                {"three", 3}
            };
            var bannedWords = new Dictionary<string, int>();
            var result = new List<string> {"three"};
            CollectionAssert.AreEqual(TestGetTopWords(words, bannedWords, 1), result);
        }

        [TestMethod]
        public void TopWords_shouldBeSorted()
        {
            var words = new Dictionary<string, int>
            {
                {"one", 4},
                {"two", 2},
                {"three", 7}
            };
            var bannedWords = new Dictionary<string, int>();
            var result = new List<string> { "three", "one", "two" };
            CollectionAssert.AreEqual(TestGetTopWords(words, bannedWords, 3), result);
        }
    }

    [TestClass]
    public class ImageCreatingTests
    {
        [TestMethod]
        public void Image_shouldBeCreated()
        {
            if (File.Exists("tests/image.png"))
                File.Delete("tests/image.png");
            Program.Main(new [] { "tests/text.txt", "tests/banned.txt", "tests/config.txt", "tests/image" });
            Assert.IsTrue(File.Exists("tests/image.png"));
        }
    }

    [TestClass]
    public class CloudDrawerTests
    {
        [TestMethod]
        public void Should_SelectAvailablePoint()
        {
            var cloudDrawer = new RandomCloudDrawer();
            var resultPoint = cloudDrawer.DefineArea(new HashSet<RectangleF> {new RectangleF(0, 0, 10, 10)},
                new SizeF(10, 10),
                new Point(30, 30));
            Assert.IsTrue((resultPoint.X >= 10 && resultPoint.X <= 20) ||
                (resultPoint.Y >= 10 && resultPoint.Y <= 20));
        }
    }
}
