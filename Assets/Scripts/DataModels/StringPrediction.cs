using System;
using Microsoft.ML.Data;

namespace PFVR.DataModels
{
    public class StringPrediction {
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
