using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.DataModels {
    [Serializable]
    public class OneHand {
        [ColumnName("PinkyProximal"), LoadColumn(0)]
        public float pinkyProximal { get; set; }

        [ColumnName("PinkyMedial"), LoadColumn(1)]
        public float pinkyMedial { get; set; }

        [ColumnName("RingProximal"), LoadColumn(2)]
        public float ringProximal { get; set; }

        [ColumnName("RingMedial"), LoadColumn(3)]
        public float ringMedial { get; set; }

        [ColumnName("MiddleProximal"), LoadColumn(4)]
        public float middleProximal { get; set; }

        [ColumnName("MiddleMedial"), LoadColumn(5)]
        public float middleMedial { get; set; }

        [ColumnName("IndexProximal"), LoadColumn(6)]
        public float indexProximal { get; set; }

        [ColumnName("IndexMedial"), LoadColumn(7)]
        public float indexMedial { get; set; }

        [ColumnName("ThumbProximal"), LoadColumn(8)]
        public float thumbProximal { get; set; }

        [ColumnName("ThumbMedial"), LoadColumn(9)]
        public float thumbMedial { get; set; }
        
        [ColumnName("WristX"), LoadColumn(10)]
        public float wristX { get; set; }

        [ColumnName("WristY"), LoadColumn(11)]
        public float wristY { get; set; }

        [ColumnName("WristZ"), LoadColumn(12)]
        public float wristZ { get; set; }

        [ColumnName("Gesture"), LoadColumn(14)]
        public string gesture { get; set; }
    }
}
