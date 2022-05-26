using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing.Imaging;

namespace PROJECTPRACTICE
{
    public partial class Writeonimages : Form
    {
        string imgname = "";
        Image<Bgr, byte> image1;
        Image<Bgr, byte> image2;
        private Dashboard mainform = null;
        public Writeonimages(Form callingform)
        {
            mainform = callingform as Dashboard;
            InitializeComponent();
        }

        private void bntloadimg_Click(object sender, EventArgs e)
        {
            OpenFileDialog emf = new OpenFileDialog();
            try
            {
                if (emf.ShowDialog() == DialogResult.OK)
                {
                    imgname = emf.FileName;
                    image1 = new Image<Bgr, byte>(emf.FileName);
                    imageBox1.Image = image1;
                    btnaddwatermark.Enabled = true;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnaddwatermark_Click(object sender, EventArgs e)
        {
            TextBox1.Visible = true;
            TextBox1.Focus();                     
        }

        private void btnsaveimg_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"PNG|*.png" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageBox2.Image.Save(saveFileDialog.FileName);
                    image1.Dispose();
                    image2.Dispose();
                }
            }
        }

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            TextBox1.Visible = false;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TextBox1.Text != "")
                {
                    image2 = new Image<Bgr, byte>(imgname);
                    CvInvoke.PutText(image2, TextBox1.Text, new Point(50, 170), FontFace.HersheySimplex, 7.0, new MCvScalar(0, 0, 255), 8);
                    imageBox2.Image = image2;
                    CvInvoke.WaitKey();                  
                    btnsaveimg.Enabled = true;
                    TextBox1.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please fill the text box");
                    TextBox1.Focus();
                }
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.mainform.show();
        }
    }
}
