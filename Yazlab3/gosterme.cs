using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab3
{
    public partial class gosterme : Form
    {

        public static int tampon = 0;
        public static int adet = 0;

        public gosterme()
        {
            InitializeComponent();
        }

        void ResimleriSay()
        {

            DirectoryInfo di = new DirectoryInfo("D:\\Sonuc\\Siyah");
            FileInfo[] rgFiles = di.GetFiles();
            adet = 0;
            for (int i = 0; i < rgFiles.Count(); i++)
            {
                adet++;
            }
        }

        private void gosterme_Load(object sender, EventArgs e)
        {
            ResimleriSay();
            timer1.Enabled = true;
            timer1.Interval = 50;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tampon++;
            if (tampon > adet)
                tampon = 1;
            pictureBox1.Image = Image.FromFile("D:\\Sonuc\\Siyah\\bitmap_" + tampon + ".bmp");
            pictureBox2.Image = Image.FromFile("D:\\Sonuc\\Renkli\\bitmap_" + tampon + ".bmp");
            ResimleriSay();
        }

    }
}

