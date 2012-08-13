using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PrintMouse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonReferenceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "*.bmp";
            ofd.InitialDirectory = @"C:\Users\user\Desktop";
            ofd.Filter = "ビットマップ ファイル (*.bmp)|*.bmp|すべてのファイル (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "プリントするファイルを開く";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxBmpFilePath.Text = ofd.FileName;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                Bitmap bitmap;
                int rest_time;
                try
                {
                    bitmap = new Bitmap(textBoxBmpFilePath.Text);
                    rest_time = int.Parse(RestTime.Text);
                }
                catch (FormatException)
                {
                    RestTime.Text = "FormatException";
                    return;
                }
                catch (ArgumentNullException)
                {
                    RestTime.Text = "NullException";
                    return;
                }
                catch (OverflowException)
                {
                    RestTime.Text = "OverflowException";
                    return;
                }
                catch (ArgumentException)
                {
                    RestTime.Text = "ArgumentException";
                    return;
                }
                if (rest_time < 1)
                {
                    rest_time = 1;
                }
                else if (rest_time > 100)
                {
                    rest_time = 100;
                }
                Program.Captcha(bitmap, rest_time);
            }
        }
    }
}
