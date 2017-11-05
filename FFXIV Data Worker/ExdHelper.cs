using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaintCoinach.Ex;
using SaintCoinach.Xiv;
using System.IO;

namespace FFXIV_Data_Worker
{
    class ExdHelper
    {
        public static void WriteRows(StreamWriter s, ISheet sheet, Language language, IEnumerable<int> colIndices, bool writeRaw)
        {
            if (sheet.Header.Variant == 1)
                WriteRowsCore(s, sheet.Cast<IRow>(), language, colIndices, writeRaw, WriteRowKey);
            else
            {
                var rows = sheet.Cast<XivRow>().Select(_ => (SaintCoinach.Ex.Variant2.DataRow)_.SourceRow);
                foreach (var parentRow in rows.OrderBy(_ => _.Key))
                    WriteRowsCore(s, parentRow.SubRows, language, colIndices, writeRaw, WriteSubRowKey);
            }
        }

        static void WriteRowsCore(StreamWriter s, IEnumerable<IRow> rows, Language language, IEnumerable<int> colIndices, bool writeRaw, Action<StreamWriter, IRow> writeKey)
        {
            foreach (var row in rows.OrderBy(_ => _.Key))
            {
                var useRow = row;

                if (useRow is IXivRow)
                    useRow = ((IXivRow)row).SourceRow;
                var multiRow = useRow as IMultiRow;

                writeKey(s, useRow);
                foreach (var col in colIndices)
                {
                    object v;

                    if (language == Language.None || multiRow == null)
                        v = writeRaw ? useRow.GetRaw(col) : useRow[col];
                    else
                        v = writeRaw ? multiRow.GetRaw(col, language) : multiRow[col, language];

                    if (v == null)
                        s.Write(",");
                    else if (IsUnescaped(v))
                        s.Write(",{0}", v);
                    else
                        s.Write(",\"{0}\"", v.ToString().Replace("\"", "\"\""));
                }
                s.WriteLine();

                s.Flush();
            }
        }

        static void WriteRowKey(StreamWriter s, IRow row) => s.Write(row.Key);

        static void WriteSubRowKey(StreamWriter s, IRow row)
        {
            var subRow = (SaintCoinach.Ex.Variant2.SubRow)row;
            s.Write(subRow.FullKey);
        }

        static bool IsUnescaped(object self)
        {
            return (self is Boolean
                || self is Byte
                || self is SByte
                || self is Int16
                || self is Int32
                || self is Int64
                || self is UInt16
                || self is UInt32
                || self is UInt64
                || self is Single
                || self is Double);
        }
    }
}