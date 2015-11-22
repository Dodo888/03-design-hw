using System.Collections.Generic;
using System.Drawing;

namespace WordsCloudGenerator.CloudDrawers
{
    public interface ICloudDrawer
    {
        Bitmap FormCloud(Bitmap bitmap, Configuration config, List<string> words);
    }
}