using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(MeshFilter))]
    public class ReverseNormals : MonoBehaviour {
        void Start() {
            var filter = GetComponent<MeshFilter>();
            if (filter != null) {
                var mesh = filter.mesh;
                var normals = mesh.normals;
                for (int i = 0; i < normals.Length; i++) {
                    normals[i] = -normals[i];
                }
                mesh.normals = normals;

                for (int m = 0; m < mesh.subMeshCount; m++) {
                    int[] triangles = mesh.GetTriangles(m);
                    for (int i = 0; i < triangles.Length; i += 3) {
                        int temp = triangles[i + 0];
                        triangles[i + 0] = triangles[i + 1];
                        triangles[i + 1] = temp;
                    }
                    mesh.SetTriangles(triangles, m);
                }
            }
        }
    }
}