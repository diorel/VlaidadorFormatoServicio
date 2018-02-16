
using Microsoft.Cognitive.CustomVision.Prediction;
using Microsoft.Cognitive.CustomVision.Training;
using Microsoft.Cognitive.CustomVision.Training.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CustomVision.Sample
{
    class Program
    {
        private static List<string> hemlockImages;

        private static List<string> japaneseCherryImages;

        private static MemoryStream testImage;

        static void Main(string[] args)
        {

            // Agregue su clave de entrenamiento desde la página de configuración del portale
            string trainingKey = "8f15a89a49a44979bf478d9391a45cd6";


            // Crea el Api, pasando la clave de entrenamiento
            TrainingApi trainingApi = new TrainingApi() { ApiKey = trainingKey };

            // Create a new project
            Console.WriteLine("Creating new project:");
            var project = trainingApi.CreateProject("My New Project");


            // Crea un nuevo proyecto
            var hemlockTag = trainingApi.CreateTag(project.Id, "Hemlock");
            var japaneseCherryTag = trainingApi.CreateTag(project.Id, "Japanese Cherry");

            // Add some images to the tags
            Console.WriteLine("\tUploading images");
            LoadImagesFromDisk();


            // Las imágenes se pueden cargar de a una por vez
            foreach (var image in hemlockImages)
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(image)))
                {
                    trainingApi.CreateImagesFromData(project.Id, stream, new List<string>() { hemlockTag.Id.ToString() });
                }
            }

            // O subido en un solo lote
            var imageFiles = japaneseCherryImages.Select(img => new ImageFileCreateEntry(Path.GetFileName(img), File.ReadAllBytes(img))).ToList();
            trainingApi.CreateImagesFromFiles(project.Id, new ImageFileCreateBatch(imageFiles, new List<Guid>() { japaneseCherryTag.Id }));


            // Ahora hay imágenes con etiquetas que comienzan a entrenar el proyecto
            Console.WriteLine("\tTraining");
            var iteration = trainingApi.TrainProject(project.Id);


            // La iteración devuelta estará en progreso, y se puede consultar periódicamente para ver cuándo se completó
            while (iteration.Status == "Training")
            {
                Thread.Sleep(1000);

                // Vuelva a consultar la iteración para obtener su estado actualizado
                iteration = trainingApi.GetIteration(project.Id, iteration.Id);
            }


            // La iteración ahora está entrenada. Convertirlo en el punto final predeterminado del proyecto
            iteration.IsDefault = true;
            trainingApi.UpdateIteration(project.Id, iteration.Id, iteration);
            Console.WriteLine("Done!\n");


            // Ahora hay un punto final entrenado, se puede usar para hacer una predicción

            // Agregue su clave de predicción desde la página de configuración del portal
            // La clave de predicción se usa en lugar de la clave de entrenamiento cuando se hacen predicciones
            string predictionKey = "559018cc3d434cef8095da2e8b8dd30c";


            // Crear un punto final de predicción, pasando la clave de predicción obtenida
            PredictionEndpoint endpoint = new PredictionEndpoint() { ApiKey = predictionKey };


            // Hacer una predicción contra el nuevo proyecto
            Console.WriteLine("Making a prediction:");
            var result = endpoint.PredictImage(project.Id, testImage);


            // Pasa el cursor sobre cada predicción y escribe los resultados
            foreach (var c in result.Predictions)
            {
                Console.WriteLine($"\t{c.Tag}: {c.Probability:P1}");
            }
            Console.ReadKey();
        }

        private static void LoadImagesFromDisk()
        {
            // this loads the images to be uploaded from disk into memory
            hemlockImages = Directory.GetFiles(@"..\..\..\Images\Hemlock").ToList();
            japaneseCherryImages = Directory.GetFiles(@"..\..\..\Images\Japanese Cherry").ToList();
            testImage = new MemoryStream(File.ReadAllBytes(@"..\..\..\Images\Test\test_image.jpg"));
        }
    }
}
