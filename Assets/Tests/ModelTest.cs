using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Microsoft.ML;
using PFVR.DataModels;

namespace Tests
{
    public class ModelTest
    {
        private const string MODEL_FILEPATH = @"/Models/manus.zip";
        private static string GetAbsolutePath(string relativePath) {
            return Application.dataPath + relativePath;
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ModelTestSimplePasses()
        {
            MLContext mlContext = new MLContext();

            ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<OneHand, StringPrediction>(mlModel);

            // Create sample data to do a single prediction with it 
            var sampleData = new OneHand {
                Kleiner = 0.9f,
                Ring = 0.9f,
                Mittel = 0.1f,
                Zeige = 0.9f,
                Daumen = 0.9f,
                Geste = "Mittelfinger"
            };

            var predictionResult = predEngine.Predict(sampleData);

            Assert.AreEqual(predictionResult.Prediction, sampleData.Geste);
        }
    }
}
