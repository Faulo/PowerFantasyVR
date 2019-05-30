using Microsoft.ML.Data;

namespace PFVR.DataModels {
    public class OneHand {
        [ColumnName("Kleiner"), LoadColumn(0)]
        public float Kleiner { get; set; }


        [ColumnName("Ring"), LoadColumn(1)]
        public float Ring { get; set; }


        [ColumnName("Mittel"), LoadColumn(2)]
        public float Mittel { get; set; }


        [ColumnName("Zeige"), LoadColumn(3)]
        public float Zeige { get; set; }


        [ColumnName("Daumen"), LoadColumn(4)]
        public float Daumen { get; set; }


        [ColumnName("Geste"), LoadColumn(5)]
        public string Geste { get; set; }


    }
}
