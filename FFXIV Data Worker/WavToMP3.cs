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

            var reader = new AudioFileReader(waveFileName);
            if (reader.WaveFormat.Channels <= 2)
            {
                using (reader)
                {
                    using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate, tag))
                        await reader.CopyToAsync(writer);//reader.CopyTo(writer);
                }
            }
            else if (reader.WaveFormat.Channels > 2)
            {
                reader.Dispose();
                mp3FileName = string.Empty;
                SplitWav(waveFileName);
                var fileNames = MixSixChannel(waveFileName);
                foreach (var fileName in fileNames)
                {
                    using (reader = new AudioFileReader(fileName))
                    {
                        using (var writer = new LameMP3FileWriter(fileName.Replace(".wav", ".mp3"), reader.WaveFormat, bitRate, tag))
                            await reader.CopyToAsync(writer);
                        mp3FileName += fileName.Replace(".wav", ".mp3") + " ";
                    }
                }

            }
            
            return $"{mp3FileName} created.\r\n\r\n";
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                
            }
        }

        public static string[] MixSixChannel(string filename)
        {
            filename = filename.Replace(".wav", "");
            string[] filenames = new string[2];

            using (WaveFileReader input1 = new WaveFileReader($"{filename}.CH01.wav"))
            {
                using (WaveFileReader input2 = new WaveFileReader($"{filename}.CH02.wav"))
                {
                    var waveProvider = new MultiplexingWaveProvider(new IWaveProvider[] { input1, input2 }, 2);
                    waveProvider.ConnectInputToOutput(0, 0);
                    waveProvider.ConnectInputToOutput(1, 1);
                    WaveFileWriter.CreateWaveFile($"{filename}.Dungeon.wav", waveProvider);
                    filenames[0] = $"{filename}.Dungeon.wav";
                }
            }

            using (WaveFileReader input1 = new WaveFileReader($"{filename}.CH03.wav"))
            {
                using (WaveFileReader input2 = new WaveFileReader($"{filename}.CH04.wav"))
                {
                    var waveProvider = new MultiplexingWaveProvider(new IWaveProvider[] { input1, input2 }, 2);
                    waveProvider.ConnectInputToOutput(0, 0);
                    waveProvider.ConnectInputToOutput(1, 1);
                    WaveFileWriter.CreateWaveFile($"{filename}.Battle.wav", waveProvider);
                    filenames[1] = $"{filename}.Battle.wav";
                }
            }

            return filenames;

            /* TO PLAY IT OUTRIGHT:
            WasapiOut outDevice = new WasapiOut();
            outDevice.Init(waveProvider);
            outDevice.Play(); */
        }
    }
}
