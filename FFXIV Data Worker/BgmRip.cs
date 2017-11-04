using SaintCoinach.Xiv;
using System;
using System.Windows.Forms;

namespace FFXIV_Data_Worker
{
    class BgmRip
    {
        public static void RipMusic()
        {
            var bgm = Realm.realm.GameData.GetSheet("BGM");
            var successCount = 0;
            var failCount = 0;
            foreach (IXivRow song in bgm)
            {
                var filePath = song["File"].ToString();
                try
                {
                    if (string.IsNullOrWhiteSpace(filePath))
                        continue;

                    if (ExportFile(filePath, null))
                    {
                        ++successCount;
                    }
                    else
                    {
                        Console.WriteLine($"File {filePath} not found.");
                        ++failCount;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Export of {filePath} failed: {ex.Message}");
                    ++failCount;
                }

            }

            var orchestrion = Realm.realm.GameData.GetSheet("Orchestrion");
            var orchestrionPath = Realm.realm.GameData.GetSheet("OrchestrionPath");
            foreach (IXivRow orchestrionInfo in orchestrion)
            {
                var path = orchestrionPath[orchestrionInfo.Key];
                var name = orchestrionInfo["Name"].ToString();
                var filePath = path["File"].ToString();

                if (string.IsNullOrWhiteSpace(filePath))
                    continue;

                try
                {
                    if (ExportFile(filePath, name))
                    {
                        ++successCount;
                    }
                    else
                    {
                        Console.WriteLine($"File {filePath} not found.");
                        ++failCount;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Export of {filePath} failed: {ex.Message}");
                    ++failCount;
                }
            }

            MessageBox.Show($"{successCount} files exported. {failCount} failed.");
        }

        private static bool ExportFile(string filePath, string suffix)
        {
            if (!Realm.realm.Packs.TryGetFile(filePath, out var file))
                return false;

            var scdFile = new SaintCoinach.Sound.ScdFile(file);
            var count = 0;
            for (var i = 0; i < scdFile.ScdHeader.EntryCount; ++i)
            {
                var e = scdFile.Entries[i];
                if (e == null)
                    continue;

                var fileNameWithoutExtension = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), System.IO.Path.GetFileNameWithoutExtension(filePath));
                if (suffix != null)
                    fileNameWithoutExtension += "-" + suffix;
                if (++count > 1)
                    fileNameWithoutExtension += "-" + count;

                var targetPath = System.IO.Path.Combine(Realm.realm.GameVersion, fileNameWithoutExtension);

                switch (e.Header.Codec)
                {
                    case SaintCoinach.Sound.ScdCodec.MSADPCM:
                        targetPath += ".wav";
                        break;
                    case SaintCoinach.Sound.ScdCodec.OGG:
                        targetPath += ".ogg";
                        break;
                    default:
                        throw new NotSupportedException();
                }

                var fInfo = new System.IO.FileInfo(targetPath);

                if (!fInfo.Directory.Exists)
                    fInfo.Directory.Create();
                System.IO.File.WriteAllBytes(fInfo.FullName, e.GetDecoded());
            }

            return true;
        }
    }
}
