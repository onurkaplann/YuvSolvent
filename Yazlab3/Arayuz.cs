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
    public partial class Arayuz : Form
    {
        public static string en = null;
        public static string boy = null;
        public static string text = null;

        public Arayuz()
        {
            InitializeComponent();
        }

        public void degerler()
        {
             en = textBox1.Text;
             boy = textBox2.Text;
             text = comboBox1.Text;       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            degerler();

            try
            {
                Directory.Delete(@"D:\\Sonuc\\Siyah", true);
                Directory.Delete(@"D:\\Sonuc\\Renkli", true);

                Directory.CreateDirectory(@"D:\\Sonuc\\Siyah");
                Directory.CreateDirectory(@"D:\\Sonuc\\Renkli");

               // MessageBox.Show("Dosya Başarıyla Silindi");
            }
            catch (Exception)
            {
               // MessageBox.Show("Dosya Silinemedi");
            }

            dosya t = new dosya();
            t.Show();
            this.Visible = false;
        }

    }
}
