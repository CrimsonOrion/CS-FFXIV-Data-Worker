using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Data_Worker
{
    class JoinWav
    {
        WaveFileWriter dest;
        WaveFileReader src1;
        WaveFileReader src2;
        string src1_n = null;
        string src2_n = null;
        string dest_n = null;
        void read_file()
        {
            Console.Out.WriteLine("Please enter two sources to join, the second one will be joined onto the first one.");
            do
            {
                if (src1_n != null || src2_n != null)
                    Console.Out.WriteLine("Files don't exist, please try again.");
                src1_n = Console.In.ReadLine();
                src2_n = Console.In.ReadLine();
            } while (!(File.Exists(src1_n) && File.Exists(src2_n)));


            Console.Out.WriteLine("Please enter the destinated file:");
            do
            {
                if (dest_n != null)
                    Console.Out.WriteLine("File must end with \'.wav\'.");
                dest_n = Console.In.ReadLine();
            } while (!dest_n.EndsWith(".wav"));
        }


        void Join()
        {
            src1 = new WaveFileReader(src1_n);
            src2 = new WaveFileReader(src2_n);


            // Test whether two wave files have the same format
            if (!src1.WaveFormat.Equals(src2.WaveFormat))
            {
                Console.Out.WriteLine("Two waves are not of the same format.");
                return;
            }


            // Read in the wave file and write out to the destination
            using (dest = new WaveFileWriter(dest_n, src1.WaveFormat))
            {
                byte[] buffer = new byte[2048];
                int n = 0;
                do
                {
                    n = src1.Read(buffer, 0, 2048);
                    n = Math.Min(2048, n);
                    dest.Write(buffer, 0, n);
                } while (n > 0);


                do
                {
                    n = src2.Read(buffer, 0, 2048);
                    n = Math.Min(2048, n);
                    dest.Write(buffer, 0, n);
                } while (n > 0);
            }
        }


        public JoinWav(string src1, string src2, string dest)
        {
            src1_n = src1;
            src2_n = src2;
            dest_n = dest;
            Join();
        }
    }
}
