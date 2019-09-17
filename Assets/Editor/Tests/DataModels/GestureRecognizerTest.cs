using Microsoft.ML;
using Microsoft.ML.Data;
using NUnit.Framework;
using PFVR.DataModels;
using PFVR.ScriptableObjects;
using System.Linq;
using UnityEngine;

namespace Tests.DataModels {
    public class GestureRecognizerTest {
        private GestureProfile profile => Resources.LoadAll<GestureProfile>("ScriptableObjects")[0];

        [Test]
        public void TestCreateMLContext() {
            var mlContext = new MLContext();
            Assert.IsInstanceOf<MLContext>(mlContext);
        }

        [Test]
        public void TestLoadModel() {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load(profile.modelDataPath, out DataViewSchema inputSchema);
            Assert.IsInstanceOf<ITransformer>(model);
            Assert.IsInstanceOf<DataViewSchema>(inputSchema);
        }

        [Test]
        public void TestCreatePredictionEngine() {
            var mlContext = new MLContext();
            var model = mlContext.Model.Load(profile.modelDataPath, out DataViewSchema inputSchema);
            var engine = mlContext.Model.CreatePredictionEngine<GestureModel, StringPrediction>(model);
            Assert.IsInstanceOf<PredictionEngine<GestureModel, StringPrediction>>(engine);
        }

        [Test]
        public void TestAllGestureSets() {
            var profiles = Resources.LoadAll<GestureProfile>("ScriptableObjects");
            foreach (var profile in profiles) {
                var mlContext = new MLContext();
                var mlModel = mlContext.Model.Load(profile.modelDataPath, out var inputSchema);
                var predEngine = mlContext.Model.CreatePredictionEngine<GestureModel, StringPrediction>(mlModel);
                var sampleData = new GestureModel {
                    gesture = "",
                    indexMedial = Random.value,
                    indexProximal = Random.value,
                    middleMedial = Random.value,
                    middleProximal = Random.value,
                    pinkyMedial = Random.value,
                    pinkyProximal = Random.value,
                    ringMedial = Random.value,
                    ringProximal = Random.value,
                    thumbMedial = Random.value,
                    thumbProximal = Random.value
                };
                var predictionResult = predEngine.Predict(sampleData);

                Assert.Contains(predictionResult.Prediction, profile.gestureSet.gestureNames.ToList());
            }
        }
    }
}
