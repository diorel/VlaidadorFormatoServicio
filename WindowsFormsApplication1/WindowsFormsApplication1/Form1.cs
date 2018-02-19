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


               respuesta = servicio.ValidarFormatoINE(byteData);

                label1.Text = respuesta.ToString();

            }


        }
    }
}
