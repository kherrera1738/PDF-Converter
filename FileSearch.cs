using System;
using System.IO;
using System.Collections.Generic;

namespace PDF_converter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            string currentDirectory = ".";
            Queue<string> directories = new Queue<string>();
            List<string> files = new List<string>();
            directories.Enqueue(currentDirectory);

            while(directories.Count > 0){
                currentDirectory = directories.Dequeue();
                files.AddRange(Directory.EnumerateFiles(currentDirectory, "*.jpg"));
                foreach(string dir in Directory.EnumerateDirectories(currentDirectory)) {
                    directories.Enqueue(dir);
                }                

            }

            foreach (string file in files) {
                Console.WriteLine(file);
            }
        }
    }
}
