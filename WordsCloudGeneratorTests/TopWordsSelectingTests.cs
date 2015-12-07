using FakeItEasy;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordsCloudGenerator;
using WordsCloudGenerator.Applications;
using WordsCloudGenerator.FileParsers;

namespace WordsCloudGeneratorTests
{
    [TestClass]
    public class TopWordsSelectingTests
    {
        public List<string> TestGetTopWords(Dictionary<string, int> words, Dictionary<string, int> banned, int amount)
        {
            var args = A.Fake<CommandLineArgs>();
            var config = A.Fake<Configuration>();
            config.WordsAmount = amount;
            var fileParser = A.Fake<IFileParser>();
            var application = A.Fake<IApplicationType>();
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
            var expected = new List<string>{"three", "two"};
            var actual = TestGetTopWords(words, bannedWords, 10);
            CollectionAssert.AreEqual(expected, actual);
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
            var expected = new List<string> {"three"};
            var actual = TestGetTopWords(words, bannedWords, 1);
            CollectionAssert.AreEqual(expected, actual);
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
            var expected = new List<string> { "three", "one", "two" };
            var actual = TestGetTopWords(words, bannedWords, 3);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
