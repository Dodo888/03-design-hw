namespace WordsCloudGenerator
{
    public class CommandLineArgs
    {
        private readonly string[] args;

        public CommandLineArgs(params string[] args)
        {
            this.args = args;
        }

        public string TextFile
        {
            get
            {
                if (args.Length == 0) return "Default/text.txt";
                return args[0];
            }
        }

        public string BannedWordsFile
        {
            get
            {
                if (args.Length < 2) return "Default/banned.txt";
                return args[1];
            }
        }

        public string ConfigFile
        {
            get
            {
                if (args.Length < 3) return "Default/config.txt";
                return args[2];
            }
        }

        public string ResultFile
        {
            get
            {
                if (args.Length < 4) return "image";
                return args[3];
            }
        }
    }
}
