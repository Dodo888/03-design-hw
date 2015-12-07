using Ninject;
using System.Collections.Generic;
using System.Linq;
using WordsCloudGenerator.Applications;
using WordsCloudGenerator.FileParsers;

namespace WordsCloudGenerator
{
    public class Program
    {
        public static Dictionary<string, IFileParser> Types = new Dictionary<string, IFileParser>
        {
            {"dictionary", new SimpleFileParser()}
        };

        private readonly CommandLineArgs Arguments;
        private readonly Configuration Configuration;
        private readonly IFileParser FileParser;
        private readonly IApplicationType ApplicationType;

        public Program(CommandLineArgs arguments, Configuration configuration, IFileParser fileParser, IApplicationType applicationType)
        {
            Arguments = arguments;
            Configuration = configuration;
            FileParser = fileParser;
            ApplicationType = applicationType;
        }

        public static void Main(string[] args)
        {
            var configFile = args.Length >= 2 ? args[2] : "Default/config.txt";
            var kernel = new Ninject.StandardKernel();
            kernel.Bind<CommandLineArgs>().ToConstant(new CommandLineArgs(args));
            kernel.Bind<Configuration>().ToConstant(new Configuration(configFile));
            kernel.Bind<IFileParser>().To<SimpleFileParser>();
            kernel.Bind<IApplicationType>().To<ConsoleApplication>();
            kernel.Get<Program>().Run(kernel);
        }

        public void Run(StandardKernel kernel)
        {
            var wordsDictionary = FileParser.GetWordsDictionaryFromFile(Arguments.TextFile);
            var bannedWordsDictionary = FileParser.GetWordsDictionaryFromFile(Arguments.BannedWordsFile);
            var topWords = GetTopWords(wordsDictionary, bannedWordsDictionary);
            ApplicationType.CreateImage(Arguments.ResultFile, topWords);
        }

        public List<string> GetTopWords(Dictionary<string, int> words, Dictionary<string, int> bannedWords)
        {
            return words
                .Where(item => !bannedWords.Keys.Contains(item.Key))
                .OrderByDescending(item => item.Value)
                .Take(Configuration.WordsAmount)
                .Select(item => item.Key)
                .ToList();
        }
    }
}
