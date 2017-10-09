using System;
using System.IO;
using System.Linq;
using W3x.Sort.Domain;
using W3x.Sort.Gui;

namespace W3x.Sort.W3x
{
    public class W
    {       
        public void Process(ApplicationArguments args)
        {
            using (var progress = new ProgressBar())
            {
                Func<WRequest, string> processor;
                switch (args.Mode)
                {
                    case Mode.All:
                        processor = WProc.NumberProcessor;
                        break;
                    case Mode.ByName:
                        processor = WProc.KeyProcessor;
                        break;
                    case Mode.Ignore:
                        processor = WProc.IgnoreProcessor;
                        break;
                    default:
                        processor = WProc.NumberProcessor;
                        break;
                }

                foreach (var request in args.Files.Select((filepath, count) => new WRequest(filepath, count, args.Key)))
                {
                    ProcessFile(request, progress, processor);
                }
            }
        }

        private static void ProcessFile(WRequest request, ProgressBar progress, Func<WRequest, string> processor)
        {
            string newDirectoryName;
            try
            {
                newDirectoryName = processor?.Invoke(request);
                if (newDirectoryName == null) return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while processing: {e.Message}. Stacktrace: {e.StackTrace}");
                return;
            }         
            
            var newDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), newDirectoryName);
            if (!Directory.Exists(newDirectoryPath)) Directory.CreateDirectory(newDirectoryPath);

            var filename = Path.GetFileName(request.Filepath);
            if (filename == null) return;

            var newFilepath = Path.Combine(newDirectoryPath, filename);
            try
            {
                File.Move(request.Filepath, newFilepath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while moving file: {e.Message}. Stacktrace: {e.StackTrace}");
                return;
            }         

            progress.Report(new ProgressBarMessage((double) request.Count / 100, filename));
        }
    }
}