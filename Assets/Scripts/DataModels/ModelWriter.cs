using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PFVR.DataModels {
    public class ModelWriter<T> {
        private const string SEPARATOR = ",";
        private string fileName;
        private StreamWriter writer;
        private IEnumerable<PropertyInfo> properties;

        public ModelWriter(string path) {
            fileName = Path.GetFullPath(path) + Path.DirectorySeparatorChar + System.DateTime.Now.ToFileTime() + ".csv";
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
        private Dictionary<string, string> ToData(T model) {
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
        }
    }
}

