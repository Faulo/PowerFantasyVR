using Microsoft.ML;
using Microsoft.ML.Data;
using System;

namespace PFVR.DataModels {
    /// <summary>
    /// The target data model used by <see cref="MLContext"/>.
    /// </summary>
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
