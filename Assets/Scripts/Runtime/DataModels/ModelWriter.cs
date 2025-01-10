using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;

namespace PFVR.DataModels {
    /// <summary>
    /// A logger for data models. Takes any class and writes all their properties to a .csv file.
    /// </summary>
    /// <typeparam name="T">A <see cref="MLContext"/>-compatible source model, like <see cref="GestureModel"/>.</typeparam>
    public sealed class ModelWriter<T> {
        const string SEPARATOR = ",";
        string fileName;
        StreamWriter writer;
        IEnumerable<PropertyInfo> properties;

        public ModelWriter(string path) {
            fileName = Path.GetFullPath(path) + Path.DirectorySeparatorChar + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".csv";
            properties = typeof(T).GetProperties();
        }
        public void Append(T model) {
            if (model == null) {
                return;
            }

            var data = ToData(model);
            if (writer == null) {
                writer = File.CreateText(fileName);
                writer.WriteLine(string.Join(SEPARATOR, data.Keys));
            }

            writer.WriteLine(string.Join(SEPARATOR, data.Values));
            writer.Flush();
        }
        Dictionary<string, string> ToData(T model) {
            var dict = new Dictionary<string, string>();
            foreach (var property in properties) {
                dict[property.Name] = property.GetValue(model, null).ToString().Replace(",", "."); //hackity-hack
            }

            return dict;
        }
        public void Finish() {
            if (writer == null) {
                return;
            }

            writer.Dispose();
            writer = null;

#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }
}

