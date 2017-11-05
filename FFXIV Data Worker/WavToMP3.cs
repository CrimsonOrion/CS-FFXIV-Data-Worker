using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
