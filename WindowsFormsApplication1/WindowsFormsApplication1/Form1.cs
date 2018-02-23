using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

     
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var respuesta = "";

            using (ServiceReference1.ServiceFormatValidatorClient servicio = new ServiceReference1.ServiceFormatValidatorClient())
            {

                
               byte[] byteData = GetImageAsByteArray(@"C:\Foto\Gundam1.jpg");

                //byte[] byteData = GetImageAsByteArray(@"C:\Foto\ine1.jpg");

               //  byte[] byteData = GetImageAsByteArray(@"C:\Foto\ine6.jpg");

               //  byte[] byteData = GetImageAsByteArray(@"C:\Foto\perro.jpg");

                respuesta = servicio.ValidarFormatoINE(byteData);

                label1.Text = respuesta.ToString();

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var respuesta = "";

            OpenFileDialog Open = new OpenFileDialog();
            Open.Filter = "Archivos JPG(*.jpg )|*.jpg";
            Open.Title = "Archivos JPG";
            try
            {
                if (Open.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = Open.FileName;
                    //string imagen = openFileDialog1.FileName;

                    string imagen = Open.FileName;
                    pictureBox1.Image = Image.FromFile(imagen);

                }
                Open.Dispose();

                using (ServiceReference1.ServiceFormatValidatorClient servicio = new ServiceReference1.ServiceFormatValidatorClient())
                {
                    string ruta = Open.FileName;

                    // byte[] byteData = GetImageAsByteArray(@"C:\Foto\Gundam1.jpg");

                     byte[] byteData = GetImageAsByteArray(@ruta);

                    respuesta = servicio.ValidarFormatoINE(byteData);

                    label1.Text = respuesta.ToString();

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
            }

        }
    }
}
