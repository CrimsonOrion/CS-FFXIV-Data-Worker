using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFXIV_Data_Worker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            WeatherRate.GetWeatherRate();
            Territory.GetTerritory();
            InitializeComponent();
        }

        private void UpdateRealmButton_Click(object sender, EventArgs e)
        {
            Realm.UpdateRealm();
            MessageBox.Show("Done");
        }

        private void RipMusicButton_Click(object sender, EventArgs e)
        {
            BgmRip.RipMusic();
            MessageBox.Show("Done");
        }

        private void RipExdButton_Click(object sender, EventArgs e)
        {
            AllExd allExd = new AllExd(Realm.realm);
            allExd.ExdRip();
            MessageBox.Show("Done");
        }

        private void OggToScdButton_Click(object sender, EventArgs e)
        {
            OggToScd.MakeFiles();
            MessageBox.Show("Done");
        }

        private void GetWeatherButton_Click(object sender, EventArgs e)
        {
            Weather.GetThisWeather(DateTime.Now, new string[] { "Eastern La Noscea", "Central Shroud", "Western Thanalan" }, 60);
        }

        private async void ScdToWavButton_Click(object sender, EventArgs e)
        {
            string[] files;
            using (OpenFileDialog oFD = new OpenFileDialog() { Multiselect = true, Filter = "SCD Files | *.scd" })
            {
                if (oFD.ShowDialog() == DialogResult.OK) files = oFD.FileNames;
                else return;
            };
            foreach (var file in files)
            {
                ResultTextBox.AppendText(await RunExternalProgram.LaunchVGMStreamAsync(@"CMD",file));
            }
            
        }

        private async void WavToMP3Button_Click(object sender, EventArgs e)
        {
            string[] files;
            using (OpenFileDialog oFD = new OpenFileDialog() { Multiselect = true, Filter = "Wav Files | *.wav" })
            {
                if (oFD.ShowDialog() == DialogResult.OK) files = oFD.FileNames;
                else return;
            }
            foreach (var file in files)
            {
                ResultTextBox.AppendText(await WavToMP3.WaveToMP3Async(file, file.Replace(".wav", ".mp3")));
            }
        }
    }
}
