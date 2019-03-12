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
    public partial class dosya : Form
    {

        static double[,] YUV2RGB_CONVERT_MATRIX = new double[3, 3] { { 1, 0, 1.4022 }, { 1, -0.3456, -0.7145 }, { 1, 1.771, 0 } };

        public static string en = null;
        public static string boy = null;
        public static string text = null;
        public static string DosyaYolu = null;
        public static int frameSize = 0;
        public static byte[] yuv = new byte[frameSize];
        public static int tampon = 0;
        public static int adet = 0;
        public static int gecis = 0;
        public static int bar = 0;
        public static int width = 0;
        public static int height = 0;
        public static int imgSize = 0;

        public dosya()
        {
            InitializeComponent();
        }

        public void dosyasecme()
        {

            OpenFileDialog dosya = new OpenFileDialog();

            dosya.ShowDialog();
           // dosya.Filter = "YUV files (*.yuv)|*.yuv|All files (*.*)|*.*";
            DosyaYolu = dosya.FileName;

            en = Arayuz.en;
            boy = Arayuz.boy;
            text = Arayuz.text;

        }


        public void render()
        {
            try
            {
                 width = Convert.ToInt32(en);
                 height = Convert.ToInt32(boy);
                 imgSize = width * height;
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya Boyutunuz veya Formatınız Hatalı");
                Environment.Exit(0);
            }

            int count = 0;

            if (text.Equals("4:4:4"))
            {
                frameSize = imgSize * 3;
            }

            if (text.Equals("4:2:2"))
            {
                frameSize = imgSize * 2;
            }

            if (text.Equals("4:2:0"))
            {
                frameSize = imgSize * 3 / 2;
            }

            byte[] yuv = new byte[frameSize];
            byte[] rgb = new byte[3 * imgSize];

            String kayit = "D:\\Sonuc\\Siyah\\bitmap_0.bmp";

            try
            {
                using (FileStream fs = File.OpenRead(DosyaYolu))
                {
                    int frame = (int)fs.Length / frameSize;
                    progressBar1.Maximum = frame * 2;
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        while (br.PeekChar() != -1)
                        {
                            br.Read(yuv, 0, frameSize);

                            Bitmap bm = ConvertYUV2RGBBlack(yuv, rgb, width, height);

                            kayit = kayit.Replace(count.ToString(), (count + 1).ToString());
                            bm.Save(kayit);
                            count++;
                            progressBar1.Value = progressBar1.Value + 1;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya Boyutunuz veya Formatınız Hatalı");
                Environment.Exit(0);
            }

        }

        public void renderrenkli()
        {
            try
            {
                 width = Convert.ToInt32(en);
                 height = Convert.ToInt32(boy);
                 imgSize = width * height;
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya Boyutunuz veya Formatınız Hatalı");
                Environment.Exit(0);
            }
            
            int count = 0;

            if (text.Equals("4:4:4"))
            {
                frameSize = imgSize * 3;
            }

            if (text.Equals("4:2:2"))
            {
                frameSize = imgSize * 2;
            }

            if (text.Equals("4:2:0"))
            {
                frameSize = imgSize * 3 / 2;
            }

            byte[] yuv = new byte[frameSize];
            byte[] rgb = new byte[3 * imgSize];

            String kayit = "D:\\Sonuc\\Renkli\\bitmap_0.bmp";

            try
            {
                using (FileStream fs = File.OpenRead(DosyaYolu))
                {
                    int frame = (int)fs.Length / frameSize;
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        while (br.PeekChar() != -1)
                        {
                            br.Read(yuv, 0, frameSize);

                            Bitmap bm = ConvertYUV2RGB(yuv, rgb, width, height);

                            kayit = kayit.Replace(count.ToString(), (count + 1).ToString());
                            bm.Save(kayit);
                            count++;
                            progressBar1.Value = progressBar1.Value + 1;
                        }
                        gecis = 1;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya Boyutunuz veya Formatınız Hatalı");
                Environment.Exit(0);
            }

        }



        static Bitmap ConvertYUV2RGBBlack(byte[] yuvFrame, byte[] rgbFrame, int width, int height)
        {
            int uIndex = width * height;
            int vIndex = uIndex + ((width * height) >> 2);
            int gIndex = width * height;
            int bIndex = gIndex * 2;

            int temp = 0;

            Bitmap bm = new Bitmap(width, height);
           

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // R
                    temp = (int)(yuvFrame[y * width + x]);
                    rgbFrame[y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));
                    // G
                    temp = (int)(yuvFrame[y * width + x]);
                    rgbFrame[gIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));
                    // B
                    temp = (int)(yuvFrame[y * width + x]);
                    rgbFrame[bIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));
                    Color c = Color.FromArgb(rgbFrame[y * width + x], rgbFrame[gIndex + y * width + x], rgbFrame[bIndex + y * width + x]);
                    bm.SetPixel(x, y, c);
                }
            }
            return bm;
        }

        static Bitmap ConvertYUV2RGB(byte[] yuvFrame, byte[] rgbFrame, int width, int height)
        {
            int uIndex = width * height;
            int vIndex = uIndex + ((width * height) >> 2);
            int gIndex = width * height;
            int bIndex = gIndex * 2;

            int temp = 0;

            Bitmap bm = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // R
                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[vIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[0, 2]);
                    rgbFrame[y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp)); 
                     // G
                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[uIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[1, 1] + (yuvFrame[vIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[1, 2]);
                    rgbFrame[gIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp)); 
                      // B
                    temp = (int)(yuvFrame[y * width + x] + (yuvFrame[uIndex + (y / 2) * (width / 2) + x / 2] - 128) * YUV2RGB_CONVERT_MATRIX[2, 1]);
                    rgbFrame[bIndex + y * width + x] = (byte)(temp < 0 ? 0 : (temp > 255 ? 255 : temp));
                    Color c = Color.FromArgb(rgbFrame[y * width + x], rgbFrame[gIndex + y * width + x], rgbFrame[bIndex + y * width + x]);
                    bm.SetPixel(x, y, c);

                }
            }
            return bm;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            dosyasecme();
            render();
            renderrenkli();

            if(gecis == 1)
            {
                gosterme t = new gosterme();
                t.Show();
                this.Visible = false;
            }
        }

    }
}
