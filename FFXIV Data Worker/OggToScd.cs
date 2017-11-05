using System;
using System.IO;
using System.Windows.Forms;

namespace FFXIV_Data_Worker
{
    class OggToScd
    {
        public const int SCD_HEADER_SIZE = 0x540;
        public static string[] currentOggFile;

        private static Byte[] ReadContentIntoByteArray(string file)
        {   
            FileStream fileInputStream = null;
            byte[] bFile = new byte[new FileInfo(file).Length];

            using (fileInputStream = new FileStream(file, FileMode.Open))
            {
                fileInputStream.Read(bFile, 0, bFile.Length);
            }
            return bFile;

        }

        private static string GetFileExtension(string file)
        {
            string ext = "";
            int i = file.IndexOf('.');
            if (i > 0 && i < file.Length - 1)
            {
                ext = file.Substring(i + 1);
            }
            return ext;
        }

        private static string RenameFileExtension(string source, string newExtension)
        {
            string target;
            string currentExtension = GetFileExtension(source);
            if (currentExtension.Equals(""))
                target = source + "." + newExtension;
            else
                target = source.Replace("." + currentExtension, "." + newExtension);

            return target;
        }

        private static void Convert(string oggPath)
        {
            byte[] ogg = ReadContentIntoByteArray(oggPath);

            float volume = 1.0f;
            int numChannels = 2;
            int sampleRate = 44100;
            int loopStart = 0;
            int loopEnd = ogg.Length;

            // Create Header
            byte[] header = CreateSCDHeader(ogg.Length, volume, numChannels, sampleRate, loopStart, loopEnd);

            //Write out scd
            if (oggPath.Contains(".scd.ogg"))
            {
                oggPath = oggPath.Replace(".ogg", "");
            }
            else
            {
                oggPath = oggPath.Replace(".ogg", ".scd");
            }

            using(BufferedStream output = new BufferedStream(new FileStream(oggPath, FileMode.OpenOrCreate))){
                output.Write(header, 0, header.Length);
                output.Write(ogg, 0, ogg.Length);
            }
        }

        private static byte[] CreateSCDHeader(int oggLength, float volume, int numChannels, int sampleRate, int loopStart, int loopEnd)
        {
            byte[] scdHeader = new byte[SCD_HEADER_SIZE];
            using (FileStream inStream = new FileStream("scd_header.bin", FileMode.Open))
            {                
                try
                {
                    inStream.Read(scdHeader, 0, scdHeader.Length);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            using (MemoryStream memory = new MemoryStream(scdHeader))
            {
                using (BinaryWriter writer = new BinaryWriter(memory))
                {
                    memory.Position = 0x10;
                    writer.Write(scdHeader.Length + oggLength);
                    memory.Position = 0x1B0;
                    writer.Write(oggLength - 0x10);
                    memory.Position = 0xA8;
                    writer.Write(volume);
                    memory.Position = 0x1B4;
                    writer.Write(numChannels);
                    memory.Position = 0x1B8;
                    writer.Write(sampleRate);
                    memory.Position = 0x1C0;
                    writer.Write(loopStart);
                    memory.Position = 0x1C4;
                    writer.Write(loopEnd);
                }                
            }
            return scdHeader;
        }

        private static void SetPath()
        {
            using (OpenFileDialog oFD = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = "OGG Files | *.ogg"
            })
            {
                if (oFD.ShowDialog() == DialogResult.OK)
                    currentOggFile = oFD.FileNames;
            }
        }

        public static void MakeFiles()
        {
            int numberOfFilesSucceeded = 0;
            int numberOfFilesFailed = 0;            
            String VGAStreamPath = "D:\\Programs\\VGAStream\\test -o ";
            String batchLine = "";
            String scdPath = "";
            String wavPath = "";

            SetPath();

            foreach (string file in currentOggFile)
            {
                try
                {
                    Convert(file);
                    RenameFileExtension(file, "ogg");

                    if (file.Contains(".scd.ogg"))
                    {
                        scdPath = file.Replace(".scd.ogg", ".scd");
                        wavPath = file.Replace(".scd.ogg", ".wav");
                    }
                    else
                    {
                        scdPath = file.Replace(".ogg", ".scd");
                        wavPath = file.Replace(".ogg", ".wav");
                    }
                    numberOfFilesSucceeded++;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was a problem converting {file}.\r\n\r\n{e.Message}");
                    numberOfFilesFailed++;
                }                
            }
            MessageBox.Show($"Number of files converted:{numberOfFilesSucceeded}\r\n\r\nNumber of files failed:{numberOfFilesFailed}");
        }
    }
}
