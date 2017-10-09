using System;
using W3x.Sort.Domain;
using W3x.Sort.W3x;

namespace W3x.Sort
{
    internal class Program
    {
        private static void Main(string[] args)
        {           

            var initializer = new Initializer();
            ApplicationArguments arguments;
            try
            {
                arguments = initializer.Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }          

            var w = new W();
            w.Process(arguments);
        }       
    }
}
