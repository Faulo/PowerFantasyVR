using Microsoft.ML.Data;
using System;

namespace PFVR.DataModels {
    public class StringPrediction {
        [ColumnName("PredictedLabel")]
#pragma warning disable IDE1006 // Benennungsstile
        public String Prediction { get; set; }
#pragma warning restore IDE1006 // Benennungsstile
#pragma warning disable IDE1006 // Benennungsstile
        public float[] Score { get; set; }
#pragma warning restore IDE1006 // Benennungsstile
    }
}
