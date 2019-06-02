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
            context = new MLContext();
            var model = context.Model.Load(modelPath, out DataViewSchema inputSchema);
            engine = context.Model.CreatePredictionEngine<GestureModel, StringPrediction>(model);
        }

        public string Guess(GestureModel model) {
            return engine.Predict(model).Prediction;
        }
    }
}