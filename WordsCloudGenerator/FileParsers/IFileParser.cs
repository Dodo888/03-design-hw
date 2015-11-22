using System.Collections.Generic;

namespace WordsCloudGenerator.FileParsers
{
    public interface IFileParser
    {
        Dictionary<string, int> GetWordsDictionaryFromFile(string path);
    }
}