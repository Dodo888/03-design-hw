using System.Collections.Generic;

namespace WordsCloudGenerator.Applications
{
    public interface IApplicationType
    {
         void CreateImage(string imagePath, List<string> words);
    }
}