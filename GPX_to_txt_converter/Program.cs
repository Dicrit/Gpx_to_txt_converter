using System;


namespace GPX_to_txt_converter
{
    class Program
    {
        

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                GPXConverter.ConvertGpx(args[0]);
                return;
            }

            string inputPath;
            Console.WriteLine("Enter input path:");
            inputPath = Console.ReadLine();
            inputPath = inputPath.Trim('\"');
            GPXConverter.ConvertGpx(inputPath);


            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
