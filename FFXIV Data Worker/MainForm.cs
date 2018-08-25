using SaintCoinach;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIV_Data_Worker
{
    public partial class MainForm : Form
    {
        public ARealmReversed realm;
        public Form ThisForm;

        public MainForm()
        {
            WeatherRate.GetWeatherRate();
            Territory.GetTerritory();
            ThisForm = this;
            InitializeComponent();
        }

        private async void UpdateRealmButton_Click(object sender, EventArgs e)
        {
            if (!realm.IsCurrentVersion)
            {
                var stopwatch = new System.Diagnostics.Stopwatch();                
                ResultTextBox.Text = $"Currently Updating...\r\n";
                //Console.SetOut(new ControlWriter(ResultTextBox));
                stopwatch.Start();
                //var update = realm.Update(true, new ConsoleProgressReporter());
                var update = await UpdateData();                
                stopwatch.Stop();
                ResultTextBox.AppendText($"\r\nList of Change:\r\n\r\n");
                foreach (var change in update.Changes)
                {
                    ResultTextBox.AppendText($"{change}\r\n");
                }
                ResultTextBox.AppendText($"Total Update time:{stopwatch.Elapsed}\r\nCurrent Version: {realm.DefinitionVersion}.\r\n");
            }
            else
                ResultTextBox.AppendText($"No update available!\r\n\r\n");
                        
        }

        private void RipMusicButton_Click(object sender, EventArgs e) => ResultTextBox.AppendText(BgmRip.RipMusic());

        private void RipExdButton_Click(object sender, EventArgs e)
        {
            AllExd allExd = new AllExd(Realm.realm);
            allExd.ExdRip();
            MessageBox.Show("Done");
        }

        private void OggToScdButton_Click(object sender, EventArgs e) => ResultTextBox.Text = OggToScd.MakeFiles();

        private void GetWeatherButton_Click(object sender, EventArgs e) => ResultTextBox.Text = Weather.GetThisWeather(DateTime.Now.AddDays(Convert.ToDouble(WeatherForcastDaysTextbox.Text)), new string[] { TerritoryComboBox.Text }, Convert.ToInt32(WeatherForcastsTextbox.Text));
        
        private async void OggToWavButton_Click(object sender, EventArgs e)
        {
            List<string> files = new List<string>();            
            using (OpenFileDialog oFD = new OpenFileDialog() { Multiselect = true, Filter = "OGG Files | *.ogg" })
            {
                if (oFD.ShowDialog() == DialogResult.OK) files.AddRange(oFD.FileNames);
                else return;
            };
            foreach (var file in files)
            {
                ResultTextBox.AppendText(await RunExternalProgram.LaunchVGMStreamAsync(@"CMD", file));
            }
        }

        private async void WavToMP3Button_Click(object sender, EventArgs e)
        {
            string[] files;
            string album;
            string year;
            using (OpenFileDialog oFD = new OpenFileDialog() { Multiselect = true, Filter = "Wav Files | *.wav" })
            {
                if (oFD.ShowDialog() == DialogResult.OK) files = oFD.FileNames;
                else return;
            }
            foreach (var file in files)
            {
                if (file.Contains("_EX2_")) { album = "FFXIV:SB DAT Rip"; year = "2017"; }
                else if (file.Contains("_EX1_")) { album = "FFXIV:HW DAT Rip"; year = "2015"; }
                else if (file.Contains("_ORCH_")) { album = "FFXIV:ORCH DAT Rip"; year = "2017"; }
                else { album = "FFXIV:ARR DAT Rip"; year = "2013"; }

                ResultTextBox.AppendText(await WavToMP3.WaveToMP3Async(file, file.Replace(".wav", ".mp3"),albumArtist: "Square Enix", album: album, year: year));
            }
            ResultTextBox.AppendText($"Completed MP3 Conversion. {files.Length} converted.\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = realm.Packs.DataDirectory.Parent;
            var y = realm.GameData.AvailableSheets;
        }

        private static bool SetGameLocation()
        {
            bool result = false;
            using (FolderBrowserDialog fBD = new FolderBrowserDialog() { Description = "Select your FFXIV folder." })
            {
                if (fBD.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.GameDirectory = fBD.SelectedPath;
                    Properties.Settings.Default.Save();
                    result = true;
                }
            }

            return result;
        }

        private async Task<UpdateReport> UpdateData()
        {   
            Task<UpdateReport> update = Task.Run(() => realm.Update(true, new ConsoleProgressReporter()));
            await update;
            return update.Result;            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var gameDirectory = Properties.Settings.Default.GameDirectory;
            if (!Directory.Exists(gameDirectory))
            {
                if (SetGameLocation() == false)
                {
                    ResultTextBox.AppendText("No FFXIV Data located.");
                    return;
                }
                gameDirectory = Properties.Settings.Default.GameDirectory;
            }
            
            realm = new ARealmReversed(gameDirectory, @"SaintCoinach.History.zip", SaintCoinach.Ex.Language.English);
            realm.Packs.GetPack(new SaintCoinach.IO.PackIdentifier("exd", SaintCoinach.IO.PackIdentifier.DefaultExpansion, 0)).KeepInMemory = true;

            ResultTextBox.AppendText($"Game version: {realm.GameVersion}\r\nDefinition Version: {realm.DefinitionVersion}\r\n\r\n");
            if (!realm.IsCurrentVersion)
            {
                ResultTextBox.AppendText("Update available.");
            }

            foreach (var item in Territory.territory)
            {
                TerritoryComboBox.Items.Add(item.Value.PlaceName);
            }
        }
    }
}
