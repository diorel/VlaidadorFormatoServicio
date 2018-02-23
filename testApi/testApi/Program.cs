using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using System.Drawing.Imaging;


using Newtonsoft.Json;
using System.Globalization;
using System.Drawing;

namespace testApi
{
    class Program
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
            public double Probability { get; set; }
        }



        //public class MyList
        //{
        //    public string Etiqueta { get; set; }
        //    public string Probabilidad { get; set; }
        //}








        static void Main(string[] args)
        {
            Console.Write("Enter image file path: ");
            string imageFilePath = Console.ReadLine();

            MakePredictionRequest(imageFilePath).Wait();

            Console.WriteLine("\n\n\nHit ENTER to exit...");
            Console.ReadLine();
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }



        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }



        static async Task MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid subscription key.
            client.DefaultRequestHeaders.Add("Prediction-Key", "559018cc3d434cef8095da2e8b8dd30c");

            // Prediction URL - replace this example URL with your valid prediction URL.
            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.1/Prediction/ee601a04-1f53-4b5a-91bb-e5c276ab7832/image?iterationId=c3dcc62f-485d-4f60-bfe1-cfd73befb5f6";

            HttpResponseMessage response;


            // Request body. Try this sample with a locally stored image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);


            using (var content = new ByteArrayContent(byteData))
            {
               // content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



                try
                {
                    response = await client.PostAsync(url, content);

                    Consulta model = null;

                    var respuesta = response.Content.ReadAsStringAsync();

                    respuesta.Wait();

                    model = (Consulta)JsonConvert.DeserializeObject(respuesta.Result.ToString(), new Consulta().GetType());

                    var Descripcion2 = (from cust in model.Predictions
                                        where cust.Tag == "INE"
                                        select new
                                        {
                                            Probabilidad = cust.Probability.ToString("P1")
                                        }).ToList().FirstOrDefault().Probabilidad.ToString();

                    string CadenaNumero = Convert.ToString(Descripcion2);
                    string NumeroCadena = CadenaNumero.Replace("%", "");
                    double INE = Convert.ToDouble(NumeroCadena);



                    // Console.WriteLine(await response.Content. ReadAsStringAsync());
                }

                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);

                }


            }


        }
    }
}
