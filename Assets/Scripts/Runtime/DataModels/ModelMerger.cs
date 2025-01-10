using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PFVR.DataModels {
    /// <summary>
    /// Takes a folder with many .csv files and merges them into one, using only the first .csv file's header row.
    /// </summary>
    public sealed class ModelMerger {
        string text {
            get {
                string[] assets = folders
                    .SelectMany(name => Resources.LoadAll<TextAsset>(name))
                    .Select(asset => asset.text)
                    .ToArray();
                if (assets.Count() == 0) {
                    return "";
                }

                for (int i = 1; i < assets.Length; i++) {
                    assets[i] = assets[i].Substring(assets[i].IndexOf("\n") + 1);
                }

                return string.Join("", assets);
            }
        }
        IEnumerable<string> folders;

        public ModelMerger(IEnumerable<string> folders) {
            this.folders = folders;
        }

        public void Put(string fileName) {
            File.WriteAllText(fileName, text);
#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }
}