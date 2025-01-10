using UnityEngine;

namespace PFVR.OurPhysics {
    [RequireComponent(typeof(MeshFilter))]
    public sealed class ReverseNormals : MonoBehaviour {
        void Start() {
            if (TryGetComponent<MeshFilter>(out var filter)) {
                var mesh = filter.mesh;
                var normals = mesh.normals;
                for (int i = 0; i < normals.Length; i++) {
                    normals[i] = -normals[i];
                }

                mesh.normals = normals;

                for (int m = 0; m < mesh.subMeshCount; m++) {
                    int[] triangles = mesh.GetTriangles(m);
                    for (int i = 0; i < triangles.Length; i += 3) {
                        (triangles[i + 1], triangles[i + 0]) = (triangles[i + 0], triangles[i + 1]);
                    }

                    mesh.SetTriangles(triangles, m);
                }
            }
        }
    }
}