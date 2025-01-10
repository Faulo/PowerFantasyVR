using Microsoft.ML;
using PFVR.ScriptableObjects;
using UnityEngine;

namespace PFVR.DataModels {
    /// <summary>
    /// Sets up an <see cref="MLContext"/> to guess <see cref="Gesture"/> IDs based on <see cref="GestureModel"/>s.
    /// </summary>
    public sealed class GestureRecognizer {
        MLContext context;
        PredictionEngine<GestureModel, StringPrediction> engine;

        public GestureRecognizer(string modelPath) {
            try {
                context = new MLContext();
                var model = context.Model.Load(modelPath, out var inputSchema);
                engine = context.Model.CreatePredictionEngine<GestureModel, StringPrediction>(model);
                Debug.Log("[PFVR] ML.NET is ready to roll!");
            } catch (System.Exception e) {
                Debug.LogWarning("[PFVR] Failed to set up ML.NET!");
                Debug.LogWarning(e);
            }
        }

        public string Guess(GestureModel model) {
            return engine.Predict(model).Prediction;
        }
    }
}