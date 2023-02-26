using PFVR.Player;
using PFVR.ScriptableObjects;
using UnityEditor;

namespace PFVR.Editor.ScriptableObjects {
    public class IGestureDictionaryEditor : UnityEditor.Editor {
        bool showDictionary = true;

        public override void OnInspectorGUI() {
            // Draw the default inspector
            DrawDefaultInspector();
            var gestureDictionary = target as IGestureDictionary;

            showDictionary = EditorGUILayout.BeginFoldoutHeaderGroup(showDictionary, "Active Gestures");
            if (showDictionary) {
                foreach (var gesture in gestureDictionary.possibleGestures) {
                    bool hasGesture = EditorGUILayout.ToggleLeft(gesture.name, gestureDictionary.HasGesture(gesture));
                    if (gestureDictionary.HasGesture(gesture) != hasGesture) {
                        if (hasGesture) {
                            gestureDictionary.AddGesture(gesture);
                        } else {
                            gestureDictionary.RemoveGesture(gesture);
                        }
                        EditorUtility.SetDirty(target);
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
    [CustomEditor(typeof(GestureSet), true)]
    public class GestureSetEditor : IGestureDictionaryEditor {
    }
    [CustomEditor(typeof(GestureConnector), true)]
    public class GestureConnectorEditor : IGestureDictionaryEditor {
    }
}