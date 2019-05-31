using Microsoft.ML;
using PFVR.DataModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Tracking {
    public class GestureRecognizer {
        private string modelPath {
            get {
                return Application.dataPath + @"/Models/gestures.zip";
            }
        }

        private MLContext context;
        private PredictionEngine<OneHand, StringPrediction> engine;

        public GestureRecognizer() {
            context = new MLContext();
            var model = context.Model.Load(modelPath, out DataViewSchema inputSchema);
            engine = context.Model.CreatePredictionEngine<OneHand, StringPrediction>(model);
        }

        public string Guess(OneHand hand) {
            return engine.Predict(hand).Prediction;
        }
    }
}