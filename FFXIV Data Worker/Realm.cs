using SaintCoinach;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ex = SaintCoinach.Ex;

namespace FFXIV_Data_Worker
{
    class Realm
    {   
        public static ARealmReversed realm = new ARealmReversed(Properties.Settings.Default.GameDirectory, @"SaintCoinach.History.zip", Ex.Language.English);

        public static string UpdateRealm()
        {
            string updates = string.Empty;
            if (!realm.IsCurrentVersion)
            {
                const bool IncludeDataChanges = true;
                var updateReport = realm.Update(IncludeDataChanges);
                foreach (var change in updateReport.Changes)
                {
                    updates += $"{change.SheetName} {change.ChangeType}\r\n";
                }
                return updates;
            }
            else
                return "Running current version.";
        }
    }

    class ConsoleProgressReporter : IProgress<Ex.Relational.Update.UpdateProgress>
    {
        #region IProgress<UpdateProgress> Members

        public void Report(Ex.Relational.Update.UpdateProgress value)
        {            
            Console.WriteLine(value);            
        }

        #endregion
    }
}
