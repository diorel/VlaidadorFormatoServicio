using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;



namespace apivison
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
            public string Probability { get; set; }
        }







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

        static async Task MakePredictionRequest(string imageFilePath)
        {

          


               var client = new HttpClient();

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

                //List<string> mylist = new List<string> 

                //List<BusinessUnit> listaresultados = JsonConvert.DeserializeObject<List<BusinessUnit>>(respuesta);

                model = (Consulta)JsonConvert.DeserializeObject(respuesta.Result.ToString(),new Consulta().GetType());

                //var query1 = from cust in model.Predictions
                //             where cust.Tag == "scaner"
                //            // select cust.Probability.Select();
                //select new { Probabilidad = cust.Probability}.ToString();


                //string descripcion = (from item in doc.Descendants("TipoTrabajador")
                //                      where Convert.ToInt32(item.Element("id").Value) == id
                //                      select item.Element("descripcion").Value.ToString()).FirstOrDefault();


                string Descripcion1 = (from cust in model.Predictions
                                      where cust.Tag == "scaner"
                                      select cust.Probability.ToString()).FirstOrDefault();

                string NumeroCadena = Descripcion1.Remove(4, 8);

                string Descripcion2 = (from cust in model.Predictions
                                       where cust.Tag == "scaner"
                                       select cust.Probability.ToString()).FirstOrDefault();
                string NumeroCadena2 = Descripcion1.Remove(4, 8);


                double NumeroSuma1 = Convert.ToDouble(NumeroCadena);

                double NumeroSuma2 = Convert.ToDouble(NumeroCadena2);


                double resultado = NumeroSuma1 + NumeroSuma2;
                  



                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }


        }
    }
}
