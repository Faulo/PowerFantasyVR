using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ML;
using PFVR.DataModels;
using System.Diagnostics;
using System.IO;
using PFVR.Tracking;

public class GameBehaviour : MonoBehaviour
{
    private PredictionEngine<OneHand, StringPrediction> engine;
    private const string MODEL_FILEPATH = @"/Models/manus.zip";
    public static string GetAbsolutePath(string relativePath) {
        return Application.dataPath + relativePath;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        MLContext mlContext = new MLContext();

        // Training code used by ML.NET CLI and AutoML to generate the model
        //ModelBuilder.CreateModel();

        ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
        engine = mlContext.Model.CreatePredictionEngine<OneHand, StringPrediction>(mlModel);
        
        // Create sample data to do a single prediction with it 
        var sampleData = new OneHand {
            Kleiner = 0.9f,
            Ring = 0.9f,
            Mittel = 0.1f,
            Zeige = 0.9f,
            Daumen = 0.9f,
            Geste = ""
        };

        // Try a single prediction
        Stopwatch sw = new Stopwatch();

        sw.Start();
        var predictionResult = engine.Predict(sampleData);
        sw.Stop();

        UnityEngine.Debug.Log(predictionResult.Prediction);
    }

    // Update is called once per frame
    void Update()
    {
        if (engine != null) {
            var hand = ManusConnector.leftHand;
            UnityEngine.Debug.Log(hand);
            if (hand != null) {
                var predictionResult = engine.Predict(hand);
                UnityEngine.Debug.Log(predictionResult.Prediction);
            }
        }
    }
}
