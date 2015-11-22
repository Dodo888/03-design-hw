using System.Collections.Generic;

namespace WordsCloudGenerator.Applications
{
    public interface IApplication
    {
         void CreateImage(string imagePath, List<string> words);
    }
}