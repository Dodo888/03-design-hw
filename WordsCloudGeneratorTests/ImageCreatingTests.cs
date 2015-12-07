using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordsCloudGenerator;

namespace WordsCloudGeneratorTests
{
    [TestClass]
    public class ImageCreatingTests
    {
        [TestMethod]
        public void Image_shouldBeCreated()
        {
            if (File.Exists("tests/image.png"))
                File.Delete("tests/image.png");
            Program.Main(new[] { "tests/text.txt", "tests/banned.txt", "tests/config.txt", "tests/image" });
            Assert.IsTrue(File.Exists("tests/image.png"));
        }
    }
}
