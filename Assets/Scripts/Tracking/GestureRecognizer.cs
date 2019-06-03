using Microsoft.ML;
using PFVR.DataModels;
using System.IO;
using UnityEngine;

namespace PFVR.Tracking {
    public class GestureRecognizer {
        private string modelPath {
            get {
                return Path.Combine(Application.streamingAssetsPath, "Models", "MLModel.zip");
            }
        }

        private MLContext context;
        private PredictionEngine<GestureModel, StringPrediction> engine;

        public GestureRecognizer() {
            try {
                context = new MLContext();
                var model = context.Model.Load(modelPath, out DataViewSchema inputSchema);
                engine = context.Model.CreatePredictionEngine<GestureModel, StringPrediction>(model);
                Debug.Log("[PFVR] ML.NET is ready to roll!");
            } catch(System.Exception e) {
                Debug.LogWarning("[PFVR] Failed to set up ML.NET!");
                Debug.LogWarning(e);
            }
        }

        public string Guess(GestureModel model) {
            return engine.Predict(model).Prediction;
        }
    }
}