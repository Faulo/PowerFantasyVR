using Microsoft.ML;
using NUnit.Framework;
using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System.Linq;
using UnityEngine;

namespace Tests.DataModels {
    public class GestureRecognizerTest {
        [Test]
        public void TestAllGestureSets() {
            var random = new System.Random();

            var sets = Resources.LoadAll<GestureSet>("ScriptableObjects");
            foreach (var set in sets) {
                var mlContext = new MLContext();
                var mlModel = mlContext.Model.Load(set.modelPath, out var inputSchema);
                var predEngine = mlContext.Model.CreatePredictionEngine<GestureModel, StringPrediction>(mlModel);
                var sampleData = new GestureModel {
                    gesture = "",
                    indexMedial = (float)random.NextDouble(),
                    indexProximal = (float)random.NextDouble(),
                    middleMedial = (float)random.NextDouble(),
                    middleProximal = (float)random.NextDouble(),
                    pinkyMedial = (float)random.NextDouble(),
                    pinkyProximal = (float)random.NextDouble(),
                    ringMedial = (float)random.NextDouble(),
                    ringProximal = (float)random.NextDouble(),
                    thumbMedial = (float)random.NextDouble(),
                    thumbProximal = (float)random.NextDouble()
                };
                var predictionResult = predEngine.Predict(sampleData);

                Assert.Contains(predictionResult.Prediction, set.gestureNames.ToList());
            }
        }
    }
}
