using Microsoft.ML.Data;
using System;

namespace PFVR.DataModels {
    [Serializable]
    public class GestureModel {
        [ColumnName("pinkyProximal"), LoadColumn(0)]
        public float pinkyProximal { get; set; }

        [ColumnName("pinkyMedial"), LoadColumn(1)]
        public float pinkyMedial { get; set; }

        [ColumnName("ringProximal"), LoadColumn(2)]
        public float ringProximal { get; set; }

        [ColumnName("ringMedial"), LoadColumn(3)]
        public float ringMedial { get; set; }

        [ColumnName("middleProximal"), LoadColumn(4)]
        public float middleProximal { get; set; }

        [ColumnName("middleMedial"), LoadColumn(5)]
        public float middleMedial { get; set; }

        [ColumnName("indexProximal"), LoadColumn(6)]
        public float indexProximal { get; set; }

        [ColumnName("indexMedial"), LoadColumn(7)]
        public float indexMedial { get; set; }

        [ColumnName("thumbProximal"), LoadColumn(8)]
        public float thumbProximal { get; set; }

        [ColumnName("thumbMedial"), LoadColumn(9)]
        public float thumbMedial { get; set; }

        [ColumnName("gesture"), LoadColumn(10)]
        public string gesture { get; set; }
    }
}
