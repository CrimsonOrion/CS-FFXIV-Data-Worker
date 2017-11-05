using SaintCoinach;
using SaintCoinach.Ex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Data_Worker
{
    class AllExd
    {
        private ARealmReversed Realm;

        public AllExd(ARealmReversed realm)
        {
            Realm = realm;
        }

        public bool ExdRip(string paramList = null)
        {
            const string CsvFileFormat = "exd-all/{0}{1}.csv";

            IEnumerable<string> filesToExport;

            if (string.IsNullOrWhiteSpace(paramList))
                filesToExport = Realm.GameData.AvailableSheets;
            else
                filesToExport = paramList.Split(' ').Select(_ => Realm.GameData.FixName(_));

            int successCount = 0;
            int failCount = 0;

            foreach (var name in filesToExport)
            {
                var sheet = Realm.GameData.GetSheet(name);
                foreach (var lang in sheet.Header.AvailableLanguages)
                {
                    var code = lang.GetCode();
                    if (code.Length > 0)
                        code = "." + code;
                    var target = new FileInfo(Path.Combine(Realm.GameVersion, string.Format(CsvFileFormat, name, code)));
                    try
                    {
                        if (!target.Directory.Exists)
                            target.Directory.Create();

                        SaveAsCsv(sheet, lang, target.FullName);

                        ++successCount;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Export of {0} failed: {1}", name, e.Message);
                        try { if (target.Exists) { target.Delete(); } } catch { }
                        ++failCount;
                    }
                }
            }

            Console.WriteLine("{0} files exported, {1} failed", successCount, failCount);

            return true;
        }

        public static void SaveAsCsv(SaintCoinach.Ex.Relational.IRelationalSheet sheet, Language language, string path)
        {
            using (var s = new StreamWriter(path, false, Encoding.UTF8))
            {
                var indexLine = new StringBuilder("key");
                var nameLine = new StringBuilder("#");
                var typeLine = new StringBuilder("int32");

                var colIndices = new List<int>();
                foreach (var col in sheet.Header.Columns)
                {
                    indexLine.AppendFormat(",{0}", col.Index);
                    nameLine.AppendFormat(",{0}", col.Name);
                    typeLine.AppendFormat(",{0}", col.ValueType);

                    colIndices.Add(col.Index);
                }

                s.WriteLine(indexLine);
                s.WriteLine(nameLine);
                s.WriteLine(typeLine);

                ExdHelper.WriteRows(s, sheet, language, colIndices, false);
            }
        }
    }
}
