using System;
using System.Collections.Generic;
using System.IO;

namespace W3x.Sort.Helpers
{
    public class FileHelper
    {
        public static IEnumerable<string> GetAllFiles(string path)
        {
            var queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (var subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files == null) continue;
                foreach (var file in files)
                {
                    yield return file;
                }
            }
        }
    }
}
