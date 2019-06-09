using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PFVR.DataModels {
    public class ModelMerger {
        private string text {
            get {
                var assets = folders
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
        private IEnumerable<string> folders;

        public ModelMerger(IEnumerable<string> folders) {
            this.folders = folders;
        }

        public void Put(string fileName) {
            File.WriteAllText(fileName, text);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}