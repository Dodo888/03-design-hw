using Ninject;
using System.Collections.Generic;
using System.Linq;
using WordsCloudGenerator.Applications;
using WordsCloudGenerator.FileParsers;

namespace WordsCloudGenerator
{
    internal class Program
    {
        public static Dictionary<string, IFileParser> Types = new Dictionary<string, IFileParser>
        {
            {"dictionary", new SimpleFileParser()}
        };

        private readonly CommandLineArgs Arguments;
        private readonly Configuration Configuration;
        private readonly IFileParser FileParser;
        private readonly IApplication Application;

        public Program(CommandLineArgs arguments, Configuration configuration, IFileParser fileParser, IApplication application)
        {
            Arguments = arguments;
            Configuration = configuration;
            FileParser = fileParser;
            Application = application;
        }

        private static void Main(string[] args)
        {
            var configFile = args[2];
            var kernel = new Ninject.StandardKernel();
            kernel.Bind<CommandLineArgs>().ToConstant(new CommandLineArgs(args));
            kernel.Bind<Configuration>().ToConstant(new Configuration(configFile));
            kernel.Bind<IFileParser>().To<SimpleFileParser>();
            kernel.Bind<IApplication>().To<ConsoleApplication>();
            kernel.Get<Program>().Run();
        }

        public void Run()
        {
            var wordsDictionary = FileParser.GetWordsDictionaryFromFile(Arguments.TextFile);
            var bannedWordsDictionary = FileParser.GetWordsDictionaryFromFile(Arguments.BannedWordsFile);
            var topWords = wordsDictionary
                .Where(item => !bannedWordsDictionary.Keys.Contains(item.Key))
                .OrderByDescending(item => item.Value)
                .Take(Configuration.WordsAmount)
                .Select(item => item.Key)
                .ToList();
            Application.CreateImage(Arguments.ResultFile, topWords);
        }
    }
}
