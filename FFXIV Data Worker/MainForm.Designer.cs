namespace FFXIV_Data_Worker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.UpdateRealmButton = new System.Windows.Forms.Button();
            this.RipMusicButton = new System.Windows.Forms.Button();
            this.RipExdButton = new System.Windows.Forms.Button();
            this.OggToScdButton = new System.Windows.Forms.Button();
            this.GetWeatherButton = new System.Windows.Forms.Button();
            this.OggToWavButton = new System.Windows.Forms.Button();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.WavToMP3Button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.TerritoryComboBox = new System.Windows.Forms.ComboBox();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetGameLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WeatherForcastDaysTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WeatherForcastsTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpdateRealmButton
            // 
            this.UpdateRealmButton.Location = new System.Drawing.Point(484, 27);
            this.UpdateRealmButton.Name = "UpdateRealmButton";
            this.UpdateRealmButton.Size = new System.Drawing.Size(75, 41);
            this.UpdateRealmButton.TabIndex = 4;
            this.UpdateRealmButton.Text = "Update Realm";
            this.UpdateRealmButton.UseVisualStyleBackColor = true;
            this.UpdateRealmButton.Click += new System.EventHandler(this.UpdateRealmButton_Click);
            // 
            // RipMusicButton
            // 
            this.RipMusicButton.Location = new System.Drawing.Point(12, 27);
            this.RipMusicButton.Name = "RipMusicButton";
            this.RipMusicButton.Size = new System.Drawing.Size(75, 41);
            this.RipMusicButton.TabIndex = 0;
            this.RipMusicButton.Text = "Rip BGM";
            this.RipMusicButton.UseVisualStyleBackColor = true;
            this.RipMusicButton.Click += new System.EventHandler(this.RipMusicButton_Click);
            // 
            // RipExdButton
            // 
            this.RipExdButton.Location = new System.Drawing.Point(12, 74);
            this.RipExdButton.Name = "RipExdButton";
            this.RipExdButton.Size = new System.Drawing.Size(75, 41);
            this.RipExdButton.TabIndex = 5;
            this.RipExdButton.Text = "Rip EXDs";
            this.RipExdButton.UseVisualStyleBackColor = true;
            this.RipExdButton.Click += new System.EventHandler(this.RipExdButton_Click);
            // 
            // OggToScdButton
            // 
            this.OggToScdButton.Location = new System.Drawing.Point(93, 27);
            this.OggToScdButton.Name = "OggToScdButton";
            this.OggToScdButton.Size = new System.Drawing.Size(75, 41);
            this.OggToScdButton.TabIndex = 1;
            this.OggToScdButton.Text = "Ogg => Scd";
            this.OggToScdButton.UseVisualStyleBackColor = true;
            this.OggToScdButton.Click += new System.EventHandler(this.OggToScdButton_Click);
            // 
            // GetWeatherButton
            // 
            this.GetWeatherButton.Location = new System.Drawing.Point(9, 245);
            this.GetWeatherButton.Name = "GetWeatherButton";
            this.GetWeatherButton.Size = new System.Drawing.Size(156, 41);
            this.GetWeatherButton.TabIndex = 9;
            this.GetWeatherButton.Text = "Weather Forcast";
            this.GetWeatherButton.UseVisualStyleBackColor = true;
            this.GetWeatherButton.Click += new System.EventHandler(this.GetWeatherButton_Click);
            // 
            // OggToWavButton
            // 
            this.OggToWavButton.Location = new System.Drawing.Point(174, 27);
            this.OggToWavButton.Name = "OggToWavButton";
            this.OggToWavButton.Size = new System.Drawing.Size(75, 41);
            this.OggToWavButton.TabIndex = 2;
            this.OggToWavButton.Text = "Ogg => Wav";
            this.OggToWavButton.UseVisualStyleBackColor = true;
            this.OggToWavButton.Click += new System.EventHandler(this.OggToWavButton_Click);
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultTextBox.Location = new System.Drawing.Point(174, 74);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ReadOnly = true;
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(385, 502);
            this.ResultTextBox.TabIndex = 13;
            this.ResultTextBox.TabStop = false;
            // 
            // WavToMP3Button
            // 
            this.WavToMP3Button.Location = new System.Drawing.Point(255, 27);
            this.WavToMP3Button.Name = "WavToMP3Button";
            this.WavToMP3Button.Size = new System.Drawing.Size(75, 41);
            this.WavToMP3Button.TabIndex = 3;
            this.WavToMP3Button.Text = "Wav => MP3";
            this.WavToMP3Button.UseVisualStyleBackColor = true;
            this.WavToMP3Button.Click += new System.EventHandler(this.WavToMP3Button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TerritoryComboBox
            // 
            this.TerritoryComboBox.FormattingEnabled = true;
            this.TerritoryComboBox.Location = new System.Drawing.Point(13, 136);
            this.TerritoryComboBox.Name = "TerritoryComboBox";
            this.TerritoryComboBox.Size = new System.Drawing.Size(155, 21);
            this.TerritoryComboBox.TabIndex = 6;
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(571, 24);
            this.MainMenuStrip.TabIndex = 10;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetGameLocationToolStripMenuItem});
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.SettingsToolStripMenuItem.Text = "Settings";
            // 
            // SetGameLocationToolStripMenuItem
            // 
            this.SetGameLocationToolStripMenuItem.Name = "SetGameLocationToolStripMenuItem";
            this.SetGameLocationToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.SetGameLocationToolStripMenuItem.Text = "Set Game Location";
            // 
            // WeatherForcastDaysTextbox
            // 
            this.WeatherForcastDaysTextbox.Location = new System.Drawing.Point(13, 178);
            this.WeatherForcastDaysTextbox.Name = "WeatherForcastDaysTextbox";
            this.WeatherForcastDaysTextbox.Size = new System.Drawing.Size(56, 20);
            this.WeatherForcastDaysTextbox.TabIndex = 7;
            this.WeatherForcastDaysTextbox.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Show weather since (Days)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Number of forcasts?";
            // 
            // WeatherForcastsTextbox
            // 
            this.WeatherForcastsTextbox.Location = new System.Drawing.Point(13, 219);
            this.WeatherForcastsTextbox.Name = "WeatherForcastsTextbox";
            this.WeatherForcastsTextbox.Size = new System.Drawing.Size(56, 20);
            this.WeatherForcastsTextbox.TabIndex = 8;
            this.WeatherForcastsTextbox.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Zone for Weather Forcast";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 620);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WeatherForcastsTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WeatherForcastDaysTextbox);
            this.Controls.Add(this.TerritoryComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.WavToMP3Button);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.OggToWavButton);
            this.Controls.Add(this.GetWeatherButton);
            this.Controls.Add(this.OggToScdButton);
            this.Controls.Add(this.RipExdButton);
            this.Controls.Add(this.RipMusicButton);
            this.Controls.Add(this.UpdateRealmButton);
            this.Controls.Add(this.MainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "FFXIV Data Worker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UpdateRealmButton;
        private System.Windows.Forms.Button RipMusicButton;
        private System.Windows.Forms.Button RipExdButton;
        private System.Windows.Forms.Button OggToScdButton;
        private System.Windows.Forms.Button GetWeatherButton;
        private System.Windows.Forms.Button OggToWavButton;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Button WavToMP3Button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox TerritoryComboBox;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetGameLocationToolStripMenuItem;
        private System.Windows.Forms.TextBox WeatherForcastDaysTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox WeatherForcastsTextbox;
        private System.Windows.Forms.Label label3;
    }
}

