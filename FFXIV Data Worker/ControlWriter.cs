﻿using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FFXIV_Data_Worker
{
    public class ControlWriter : TextWriter
    {
        private Control textbox;
        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            textbox.Text += value;            
        }

        public override void Write(string value)
        {
            textbox.Text += value;
        }
        
        public override Encoding Encoding => System.Text.Encoding.ASCII;
    }
}
