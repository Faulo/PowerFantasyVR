using Microsoft.ML.Data;
using System;

namespace PFVR.DataModels {
    [Serializable]
    public class OneHandOld {
        [ColumnName("Kleiner"), LoadColumn(0)]
#pragma warning disable IDE1006 // Benennungsstile
        public float Kleiner { get; set; }
#pragma warning restore IDE1006 // Benennungsstile

        [ColumnName("Ring"), LoadColumn(1)]
#pragma warning disable IDE1006 // Benennungsstile
        public float Ring { get; set; }
#pragma warning restore IDE1006 // Benennungsstile


        [ColumnName("Mittel"), LoadColumn(2)]
#pragma warning disable IDE1006 // Benennungsstile
        public float Mittel { get; set; }
#pragma warning restore IDE1006 // Benennungsstile


        [ColumnName("Zeige"), LoadColumn(3)]
#pragma warning disable IDE1006 // Benennungsstile
        public float Zeige { get; set; }
#pragma warning restore IDE1006 // Benennungsstile


        [ColumnName("Daumen"), LoadColumn(4)]
#pragma warning disable IDE1006 // Benennungsstile
        public float Daumen { get; set; }
#pragma warning restore IDE1006 // Benennungsstile


        [ColumnName("Geste"), LoadColumn(5)]
#pragma warning disable IDE1006 // Benennungsstile
        public string Geste { get; set; }
#pragma warning restore IDE1006 // Benennungsstile
    }
}
