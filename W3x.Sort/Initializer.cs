using System;
using System.IO;
using System.Linq;
using Fclp;
using W3x.Sort.Domain;
using W3x.Sort.Helpers;

namespace W3x.Sort
{
    public class Initializer
    {
        private static readonly string[] Extentions = { "w3x", "w3n", "w3m" };

        public ApplicationArguments Run(string[] args)
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();
            parser.Setup(arg => arg.Mode)
                .As('t')
                .SetDefault(Mode.All);
            parser.Setup(arg => arg.Key)
                .As('k')
                .SetDefault(null);

            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                throw new ArgumentException("Error while parsing command line.");
            }

            var arguments = parser.Object;
            try
            {
                arguments.Files = FileHelper.GetAllFiles(Directory.GetCurrentDirectory())
                    .Where(file => Extentions.Any(file.EndsWith))
                    .ToArray();
            }
            catch (Exception e)
            {
                throw new FileLoadException($"Error while loading files from directory. Message: {e.Message}");
            }           
            return arguments;
        }
    }
}
