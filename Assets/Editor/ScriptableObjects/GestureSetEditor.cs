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

            foreach (var gesture in gestures) {
                if (EditorGUILayout.ToggleLeft(gesture.name, gestureSet.Contains(gesture))) {
                    gestureSet.Append(gesture);
                } else {
                    gestureSet.Remove(gesture);
                }
            }

            EditorUtility.SetDirty(target);
        }
    }
}