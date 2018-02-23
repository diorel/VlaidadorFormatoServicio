using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace testapi2
{
    class Program
    {
  

        static void Main(string[] args)
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("559018cc3d434cef8095da2e8b8dd30c", "{subscription key}");

            // Request parameters
            queryString["iterationId"] = "{string}";
            queryString["application"] = "{string}";
            var uri = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.1/Prediction/ee601a04-1f53-4b5a-91bb-e5c276ab7832/image?iterationId=c3dcc62f-485d-4f60-bfe1-cfd73befb5f6" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
                response = await client.PostAsync(uri, content);
            }


        }
    }
}
