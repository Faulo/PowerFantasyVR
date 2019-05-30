using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ML;
using ShaderGraphML.Model.DataModels;
using System.Diagnostics;
using System.IO;

public class GameBehaviour : MonoBehaviour
{
    private const string MODEL_FILEPATH = @"/Models/manus.zip";
    public static string GetAbsolutePath(string relativePath) {
        return Application.dataPath + relativePath;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //*
        MLContext mlContext = new MLContext();

        // Training code used by ML.NET CLI and AutoML to generate the model
        //ModelBuilder.CreateModel();

        ITransformer mlModel = mlContext.Model.Load(GetAbsolutePath(MODEL_FILEPATH), out DataViewSchema inputSchema);
        var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        
        // Create sample data to do a single prediction with it 
        ModelInput sampleData = new ModelInput {
            Kleiner = 0.9f,
            Ring = 0.9f,
            Mittel = 0.1f,
            Zeige = 0.9f,
            Daumen = 0.9f,
            Geste = "Mittelfinger"
        };

        // Try a single prediction
        Stopwatch sw = new Stopwatch();

        sw.Start();
        ModelOutput predictionResult = predEngine.Predict(sampleData);
        sw.Stop();

        UnityEngine.Debug.Log(predictionResult.Prediction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
