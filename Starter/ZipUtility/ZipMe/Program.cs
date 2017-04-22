using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace ZipMe
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length == 0)
            {
                System.Console.WriteLine("No Parameter specified!");
                System.Console.ReadLine();
            }
            else if(args.Length == 1)
            {
                CompressFiles(Directory.GetCurrentDirectory(), args[0], false);
            }
            else if (args.Length == 2)
            {
                CompressFiles(Directory.GetCurrentDirectory(), args[0], bool.Parse(args[1]));
            }
        }

        static public void CompressFiles(String directoryPath, String extToCompress, bool stripExtension)
        {
            String destFile;

            // Compress the current directory
            foreach (String origFile in Directory.GetFiles(directoryPath))
            {
                if (origFile.ToLower().EndsWith(extToCompress))
                {
                    if (!stripExtension)
                    {
                        destFile = origFile + ".zip";
                    }
                    else
                    {
                        destFile = origFile.Substring(0, origFile.ToLower().IndexOf(extToCompress) - 1) + ".zip";
                    }
                    ZipUtility.ZipManager.CompressFile(origFile, destFile);
                }
            }

            // Compress the sub directories
            foreach (String subDirectory in Directory.GetDirectories(directoryPath))
            {
                CompressFiles(subDirectory, extToCompress, stripExtension);
            }

        }
    }
}
