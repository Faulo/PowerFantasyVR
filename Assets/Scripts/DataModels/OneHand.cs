using Microsoft.ML.Data;
using System;
using System.Collections.Generic;

namespace PFVR.DataModels {
    [Serializable]
    public class OneHand {
        [ColumnName("PinkyProximal"), LoadColumn(0)]
        public float PinkyProximal { get; set; }

        [ColumnName("PinkyMedial"), LoadColumn(1)]
        public float PinkyMedial { get; set; }

        [ColumnName("RingProximal"), LoadColumn(2)]
        public float RingProximal { get; set; }

        [ColumnName("RingMedial"), LoadColumn(3)]
        public float RingMedial { get; set; }

        [ColumnName("MiddleProximal"), LoadColumn(4)]
        public float MiddleProximal { get; set; }

        [ColumnName("MiddleMedial"), LoadColumn(5)]
        public float MiddleMedial { get; set; }

        [ColumnName("IndexProximal"), LoadColumn(6)]
        public float IndexProximal { get; set; }

        [ColumnName("IndexMedial"), LoadColumn(7)]
        public float IndexMedial { get; set; }

        [ColumnName("ThumbProximal"), LoadColumn(8)]
        public float ThumbProximal { get; set; }

        [ColumnName("ThumbMedial"), LoadColumn(9)]
        public float ThumbMedial { get; set; }

        [ColumnName("Gesture"), LoadColumn(10)]
        public string Gesture { get; set; }
    }
}
