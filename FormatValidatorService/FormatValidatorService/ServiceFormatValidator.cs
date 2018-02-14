using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;



namespace FormatValidatorService
{
   public class ServiceFormatValidator : IServiceFormatValidator
    {

        public string VerificarIfe(string Foto)
        {

            // crear un objeto imagen desde archivo

            Image imagen = Image.FromFile(@"C:\Foto\Gundam1.jpg");

            // Crar Un MemoryStream

            var ms = new MemoryStream();

            // salvar los bytes  en ms

            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            // Obtenr los bytes

            var bytes = ms.ToArray();

            var imageMemoryStream = new MemoryStream(bytes);

            Image imgFormStream = Image.FromStream(imageMemoryStream);

            try
            {

                imgFormStream.Save(@"C:\Users\rcortes\Documents\GitHub\VlaidadorFormatoServicio\FormatValidatorService\FormatValidatorService\Foto\\Gundam2.jpg", ImageFormat.Jpeg);






            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return null;
        }


    }
}
