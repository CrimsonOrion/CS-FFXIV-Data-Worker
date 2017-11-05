using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WavFile;


namespace FFXIV_Data_Worker
{
    public static class WavToMP3
    {
        // Convert WAV to MP3 using libmp3lame library
        public static void WaveToMP3(string waveFileName, string mp3FileName, int bitRate = 192)
        {
            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);
        }

        // Convert MP3 file to WAV using NAudio classes only
        public static void MP3ToWave(string mp3FileName, string waveFileName)
        {
            using (var reader = new Mp3FileReader(mp3FileName))
            using (var writer = new WaveFileWriter(waveFileName, reader.WaveFormat))
                reader.CopyTo(writer);
        }

        public static async Task<string> WaveToMP3Async(string waveFileName,
            string mp3FileName,
            int bitRate = 192,
            string title = "",
            string subtitle = "",
            string comment = "",
            string artist = "",
            string albumArtist = "",
            string album = "",
            string year = "",
            string track = "",
            string genre = "",
            byte[] albumArt = null,
            string[] userDefinedTags = null)
        {
            ID3TagData tag = new ID3TagData
            {
                Title = title,
                Artist = artist,
                Album = album,
                Year = year,
                Comment = comment,
                Genre = LameMP3FileWriter.Genres[36], // 36 is game.  FUll list @ http://ecmc.rochester.edu/ecmc/docs/lame/id3.html
                Subtitle = subtitle,
                AlbumArt = albumArt,
                AlbumArtist = albumArtist,
                Track = track,
                UserDefinedTags = userDefinedTags
            };

            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate, tag))
                await reader.CopyToAsync(writer);//reader.CopyTo(writer);

            return $"{mp3FileName} created.\r\n\r\n";
        }

        public static void GetWaveChannel()
        {
            var bytes = new byte[50];
            WaveFormat waveFormat;

            using (var stream = new FileStream(@"D:\Users\eimi_\Desktop\Extract\music\ex1\BGM_EX1_Alex01.wav", FileMode.Open))
            {
                stream.Read(bytes, 0, 50);
            }

            using (var reader = new AudioFileReader(@"D:\Users\eimi_\Desktop\Extract\music\ex1\BGM_EX1_Alex01.wav"))
            {
                waveFormat = reader.WaveFormat;
            }

            var speakerMask = BitConverter.ToUInt32(new[] { bytes[40], bytes[41], bytes[42], bytes[43] }, 0);
            

        }

        public static void SplitWav(string fileName)
        {
            
            string outputPath = fileName.Remove(fileName.Length - Path.GetFileName(fileName).Length);
            
            try
            {
                long bytesTotal = 0;
                var splitter = new WavFileSplitter(
                    value => Console.Write(string.Format("\rProgress: {0:0.0}%", value)));
                var sw = Stopwatch.StartNew();
                bytesTotal = splitter.SplitWavFile(fileName, outputPath, CancellationToken.None);
                sw.Stop();
                Console.Write(Environment.NewLine);
                Console.WriteLine(
                    string.Format(
                        "Data bytes processed: {0} ({1} MB)",
                        bytesTotal, Math.Round((double)bytesTotal / (1024 * 1024), 1)));
                Console.WriteLine(string.Format("Elapsed time: {0}", sw.Elapsed));
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(-1);
            }
        }

        // This code will only return the bytes of a particular channel. It's up to you to convert the bytes to actual samples.
        public static byte[] GetChannelBytes(byte[] audioBytes, uint speakerMask, Channels channelToRead, int bitDepth, uint sampleStartIndex, uint sampleEndIndex)
        {
            var channels = FindExistingChannels(speakerMask);
            var ch = GetChannelNumber(channelToRead, channels);
            var byteDepth = bitDepth / 8;
            var chOffset = ch * byteDepth;
            var frameBytes = byteDepth * channels.Length;
            var startByteIncIndex = sampleStartIndex * byteDepth * channels.Length;
            var endByteIncIndex = sampleEndIndex * byteDepth * channels.Length;
            var outputBytesCount = endByteIncIndex - startByteIncIndex;
            var outputBytes = new byte[outputBytesCount / channels.Length];
            var i = 0;

            startByteIncIndex += chOffset;

            for (var j = startByteIncIndex; j < endByteIncIndex; j += frameBytes)
            {
                for (var k = j; k < j + byteDepth; k++)
                {
                    outputBytes[i] = audioBytes[(k - startByteIncIndex) + chOffset];
                    i++;
                }
            }

            return outputBytes;
        }

        private static Channels[] FindExistingChannels(uint speakerMask)
        {
            var foundChannels = new List<Channels>();

            foreach (var ch in Enum.GetValues(typeof(Channels)))
            {
                if ((speakerMask & (uint)ch) == (uint)ch)
                {
                    foundChannels.Add((Channels)ch);
                }
            }

            return foundChannels.ToArray();
        }

        private static int GetChannelNumber(Channels input, Channels[] existingChannels)
        {
            for (var i = 0; i < existingChannels.Length; i++)
            {
                if (existingChannels[i] == input)
                {
                    return i;
                }
            }

            return -1;
        }

        [Flags]
        public enum Channels : uint
        {
            FrontLeft = 0x1,
            FrontRight = 0x2,
            FrontCenter = 0x4,
            Lfe = 0x8,
            BackLeft = 0x10,
            BackRight = 0x20,
            FrontLeftOfCenter = 0x40,
            FrontRightOfCenter = 0x80,
            BackCenter = 0x100,
            SideLeft = 0x200,
            SideRight = 0x400,
            TopCenter = 0x800,
            TopFrontLeft = 0x1000,
            TopFrontCenter = 0x2000,
            TopFrontRight = 0x4000,
            TopBackLeft = 0x8000,
            TopBackCenter = 0x10000,
            TopBackRight = 0x20000
        }
    }
}
