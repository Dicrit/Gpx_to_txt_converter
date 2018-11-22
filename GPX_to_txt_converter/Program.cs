using System;
using System.Windows.Forms;

namespace GPX_to_txt_converter
{
    class Program
    {
        static string OpenDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "gpx files (*.gpx)|*.gpx|All files (*.*)|*.*";

                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                else if (result == DialogResult.Cancel)
                {
                    throw new Exception("");
                }
                else
                {
                    throw new Exception("Failed to get filename from dialog");
                }
            }
        }

        static bool AskForNextFile()
        {
            DialogResult result = MessageBox.Show(new Form() { TopMost = true }, "Run again for another file?", "Completed!", MessageBoxButtons.YesNo);
            return result == DialogResult.Yes;
        }


        [STAThread]
        static void Main(string[] args)
        {
            //XDD But need to Message box to be appeared
            Form form = new Form();
            form.Load += (a, b) => form.Close();
            Application.Run(form);


            if (args.Length == 1)
            {
                GPXConverter.ConvertGpx(args[0]);
                return;
            }
            string inputPath = "";
            do
            {
                try
                {
                    inputPath = OpenDialog();
                    GPXConverter.ConvertGpx(inputPath);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (AskForNextFile());
        }
    }
}
