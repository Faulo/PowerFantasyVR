using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Microsoft.ML;
using PFVR.DataModels;

namespace PFVR.ScriptableObjects {
    [CustomEditor(typeof(GestureSet))]
    public class GestureSetEditor : Editor {
        public override void OnInspectorGUI() {
            // Draw the default inspector
            DrawDefaultInspector();
            var gestureSet = target as GestureSet;
            var gestures = Resources.LoadAll<Gesture>("ScriptableObjects");

            //var context = new MLContext();
            //var model = context.Model.Load(gestureSet.modelPath, out DataViewSchema inputSchema);
            //context.CreateEnumerable<InspectedRowWithAllFeatures>(transformedData, reuseRowObject: false)
            // Take a couple values as an array.
            //.Take(4).ToArray();

            //var engine = context.Model.CreatePredictionEngine<GestureModel, StringPrediction>(model);
            //context.Data.
            //var col = engine.OutputSchema.GetColumnOrNull("gesture");
            //Debug.Log(col.Value.ToString());

            foreach (var gesture in gestures) {
                var active = EditorGUILayout.ToggleLeft(gesture.name, true);
            }

            EditorUtility.SetDirty(target);
        }
    }
}