using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

using FormatValidatorService.Funcionalidades;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FormatValidatorService
{
   public class ServiceFormatValidator : IServiceFormatValidator
    {

        public class Consulta
        {
            public string Id { get; set; }
            public string Project { get; set; }
            public string Iteration { get; set; }
            public string Created { get; set; }
            public List<Predictions> Predictions { get; set; }
        }


        public class Predictions
        {
            public string TagId { get; set; }
            public string Tag { get; set; }
            public string Probability { get; set; }
        }




        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }




        public async Task<string> ValidarFormatoINE(string imageFilePath)
        {
            var client = new HttpClient();


             // crear un objeto imagen desde archivo
            Image imagen = Image.FromFile(@"C:\Foto\Gundam1.jpg");

            // Crar Un MemoryStream
            var ms = new MemoryStream();

            // salvar los bytes  en ms

            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);



            //-------------------------- Inicia Evaluacion
            // Se obmtine los bytes ys e guarda en la varianble  "bytes"


            // Obtenr los bytes
            // descomentar para recivir imagen en bytes 
            // var bytes = imageFilePath;

            var bytes = ms.ToArray();
      
            var imageMemoryStream = new MemoryStream(bytes);
           
            Image imgFormStream = Image.FromStream(imageMemoryStream);


            //imgFormStream.Save(@"C:\Users\rcortes\Documents\GitHub\VlaidadorFormatoServicio\FormatValidatorService\FormatValidatorService\Images\Identification\ImgScan.jpg", ImageFormat.Jpeg);

            imgFormStream.Save(@"..\..\Images\Identification\ImgScan.jpg", ImageFormat.Jpeg);




            // Solicitar encabezados: reemplace esta clave de ejemplo con su clave de suscripción válida.
            client.DefaultRequestHeaders.Add("Prediction-Key", "559018cc3d434cef8095da2e8b8dd30c");

            // URL de predicción: reemplace esta URL de ejemplo con su URL de predicción válida.
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.1/Prediction/d05cabb8-3952-4334-9b40-a8afc191ce61/image?iterationId=b8221738-c389-4e06-9151-b91c7dec8a5d";

            HttpResponseMessage response;

            // cuerpo de solicitud Pruebe esta muestra con una imagen almacenada localmente.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);

                Consulta model = null;

                var respuesta = response.Content.ReadAsStringAsync();

                respuesta.Wait();


                model = (Consulta)JsonConvert.DeserializeObject(respuesta.Result.ToString(), new Consulta().GetType());


                string Descripcion1 = (from cust in model.Predictions
                                       where cust.Tag == "scaner"
                                       select cust.Probability.ToString()).FirstOrDefault();

                string NumeroCadena = Descripcion1.Remove(4, 8);

                string Descripcion2 = (from cust in model.Predictions
                                       where cust.Tag == "internet"
                                       select cust.Probability.ToString()).FirstOrDefault();
                string NumeroCadena2 = Descripcion1.Remove(4, 8);


                double NumeroSuma1 = Convert.ToDouble(NumeroCadena);

                double NumeroSuma2 = Convert.ToDouble(NumeroCadena2);


                double resultado = NumeroSuma1 + NumeroSuma2;


                string aprobada = "true";
                string rechazada = "false";
                string evaluacion = "";


                if (resultado >= 60)
                {
                    evaluacion = aprobada;
                }

                else
                {
                    evaluacion = rechazada;
                }

                return (evaluacion);

            }
        }


























        //public string VerificarIfe(string Foto)
        //{

        //    // crear un objeto imagen desde archivo

        //    Image imagen = Image.FromFile(@"C:\Foto\Gundam1.jpg");

        //    // Crar Un MemoryStream

        //    var ms = new MemoryStream();

        //    // salvar los bytes  en ms

        //    imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

        //    // Obtenr los bytes

        //    var bytes = ms.ToArray();

        //    var imageMemoryStream = new MemoryStream(bytes);

        //    Image imgFormStream = Image.FromStream(imageMemoryStream);

        //    try
        //    {

        //        //imgFormStream.Save(@"C:\Users\rcortes\Documents\GitHub\VlaidadorFormatoServicio\FormatValidatorService\FormatValidatorService\Foto\\Gundam2.jpg", ImageFormat.Jpeg);


        //       // string ruta = (@"C:\Users\rcortes\Documents\GitHub\VlaidadorFormatoServicio\FormatValidatorService\FormatValidatorService\Foto\Gundam2.jpg");

        //        CustomVision obj = new CustomVision();


        //        await obj.ValidarFormato("C:\\Users\rcortes\\Documents\\GitHub\\VlaidadorFormatoServicio\\FormatValidatorService\\FormatValidatorService\\Foto\\Gundam2.jpg");


             

            

        //     }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }

        //    return null;
        //}


    }
}
