using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace GPX_to_txt_converter
{
    class GPXConverter
    {
        static IEnumerable<string> GetAllWpts(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode gpx = doc.DocumentElement;
            foreach (XmlNode wpt in gpx.ChildNodes)
            {
                string lat = wpt.Attributes["lat"].Value;
                string lon = wpt.Attributes["lon"].Value;
                string name = wpt["name"].InnerText;
                string result = string.Format("{0},{1},{2}", name, lat, lon);
                yield return result;
            }
        }


        public static void ConvertGpx(string inputFilePath, string outputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("FAILED, file {0} doesn't exist", inputFilePath);
                return;
            }
            int current = 0;
            StreamWriter outputFile = File.CreateText(outputFilePath);
            foreach (string str in GetAllWpts(inputFilePath))
            {
                outputFile.WriteLine(str);
                if (current > 0)
                {
                    Console.CursorTop = Console.CursorTop - 1;
                }
                Console.WriteLine("Done {0}", ++current);
            }
            outputFile.Close();
            Console.WriteLine("Finished.");
        }


        public static void ConvertGpx(string inputPath)
        {
            string directory = new FileInfo(inputPath).Directory.FullName;
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = string.Format("{0}\\{1}{2}", directory, fileName, ".txt");
            ConvertGpx(inputPath, outputPath);
        }
    }
}
