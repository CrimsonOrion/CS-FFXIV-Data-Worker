using SaintCoinach;

namespace FFXIV_Data_Worker
{
    class Realm
    {
        const string GameDirectory = @"E:\Games\SquareEnix\FINAL FANTASY XIV - A Realm Reborn";
        public static ARealmReversed realm = new ARealmReversed(GameDirectory, SaintCoinach.Ex.Language.English);

        public static void UpdateRealm()
        {
            if (!realm.IsCurrentVersion)
            {
                const bool IncludeDataChanges = true;
                var updateReport = realm.Update(IncludeDataChanges);
                foreach (var change in updateReport.Changes)
                {
                    System.Windows.Forms.MessageBox.Show(change.SheetName + " " + change.ChangeType);
                }
            }
        }
    }
}
