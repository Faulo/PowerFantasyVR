using Slothsoft.UnityExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Environment {
    [ExecuteAlways]
    public class LevelObject : MonoBehaviour {
        enum LevelObjectType {
            Intangible,
            StaticCollider,
            Rigidbody,
            Tree,
            Water,
            Waterfall,
            Collectable
        }
        [SerializeField]
        private LevelObjectType type = default;

        [Space]
        [SerializeField, Range(0, 100)]
        private float rigidbodyDensity = 1;
        [SerializeField, Range(0, 100)]
        private float rigidbodyDrag = 0;
        [Space]
        [SerializeField, Range(0, 1000)]
        private int waterPlaneWidth = 0;
        [SerializeField, Range(0, 1000)]
        private int waterPlaneHeight = 0;
        [SerializeField, Range(0, 100)]
        private int waterPlaneWaveWidth = 1;
        [SerializeField, Range(0, 100)]
        private int waterPlaneWaveHeight = 1;
        [SerializeField]
        private bool waterPlaneAutoUpdate = false;
        public static string waterPlaneObjectName = "Water";
        public static string waterPlaneSaveLocation = "Assets/Meshes/Environment/Water/";
        private float waterPlaneRoutineId;
        [Space]
        [SerializeField]
        private ParticleSystem waterfallTopFoam = default;
        [SerializeField]
        private ParticleSystem waterfallBottomFoam = default;

        [Space]
        [SerializeField]
        private bool levelOfDetail = false;
        [SerializeField, Range(0, 1)]
        private float levelOfDetailCutoff = 0.005f;

        private void Awake() {
            ApplyType();
        }

        private void Update() {
            if (!Application.isPlaying) {
                ApplyType();
            }
        }

        private void ApplyType() {
            switch (type) {
                case LevelObjectType.Intangible:
                    SetStatic(true);
                    DisableColliders();
                    DisableRigidbody();
                    DisableLighting();
                    SetLayer("Ignore Raycast");
                    break;
                case LevelObjectType.StaticCollider:
                    SetStatic(true);
                    EnableColliders();
                    DisableRigidbody();
                    EnableStaticLighting();
                    SetLayer("Ground");
                    break;
                case LevelObjectType.Rigidbody:
                    SetStatic(false);
                    EnableColliders();
                    EnableRigidbody();
                    EnableDynamicLighting();
                    SetLayer("Obstacle");
                    break;
                case LevelObjectType.Tree:
                    SetStatic(true);
                    EnableColliders();
                    DisableRigidbody();
                    EnableStaticLighting();
                    SetLayer("Obstacle");
                    break;
                case LevelObjectType.Water:
                    SetStatic(false);
                    DisableColliders();
                    DisableRigidbody();
                    DisableLighting();
                    SetLayer("Water");
                    if (!Application.isPlaying && waterPlaneAutoUpdate) {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.delayCall += CreateWaterPlaneCall;
#endif
                    }
                    break;
                case LevelObjectType.Waterfall:
                    SetStatic(false);
                    DisableColliders();
                    DisableRigidbody();
                    DisableLighting();
                    SetLayer("Water");
                    if (!Application.isPlaying && waterPlaneAutoUpdate) {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.delayCall += CreateWaterPlaneCall;
                        UnityEditor.EditorApplication.delayCall += CreateWaterfallFoamCall;
#endif
                    }
                    break;
                case LevelObjectType.Collectable:
                    SetStatic(false);
                    EnableColliders();
                    DisableRigidbody();
                    DisableLighting();
                    SetLayer("Collectable");
                    break;
            }
            if (levelOfDetail) {
                EnableLevelOfDetail();
            } else {
                DisableLevelOfDetail();
            }
        }


        private void SetStatic(bool value) {
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.isStatic = value);
        }
        private void DisableLighting() {
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = false;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }
        private void EnableStaticLighting() {
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                renderer.receiveShadows = true;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }
        private void EnableDynamicLighting() {
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                renderer.receiveShadows = true;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }

        private void DisableColliders() {
            GetComponentsInChildren<Collider>().ForAll(collider => collider.enabled = false);
        }
        private void EnableColliders() {
            GetComponentsInChildren<Collider>().ForAll(collider => collider.enabled = true);
        }

        private void DisableRigidbody() {
            var rigidbody = GetComponent<Rigidbody>();
            if (rigidbody) {
                DestroyImmediate(rigidbody);
            }
        }
        private void EnableRigidbody() {
            var rigidbody = GetComponent<Rigidbody>();
            if (!rigidbody) {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }
            rigidbody.mass = rigidbodyDensity * transform.localScale.x * transform.localScale.y * transform.localScale.z;
            rigidbody.drag = rigidbodyDrag;
        }

        private void SetLayer(string layerName) {
            var layerId = LayerMask.NameToLayer(layerName);
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.layer = layerId);
        }

        private void EnableLevelOfDetail() {
            var lodGroup = GetComponent<LODGroup>();
            if (!lodGroup) {
                lodGroup = gameObject.AddComponent<LODGroup>();
            }
            var lods = new LOD[1];
            lods[0].renderers = GetComponentsInChildren<Renderer>();
            lods[0].screenRelativeTransitionHeight = levelOfDetailCutoff;
            lodGroup.SetLODs(lods);
        }
        private void DisableLevelOfDetail() {
            var lodGroup = GetComponent<LODGroup>();
            if (lodGroup) {
                DestroyImmediate(lodGroup);
            }
        }





        private void CreateWaterPlaneCall() {
            if (!this || !gameObject || !transform) {
                return;
            }
            if (waterPlaneWidth > 0 && waterPlaneHeight > 0) {
                if (transform.localScale.x != 1) {
                    waterPlaneWidth = (int)(waterPlaneWidth * transform.localScale.x);
                    transform.SetScaleX(1);
                }
                if (transform.localScale.y != 1) {
                    waterPlaneHeight = (int)(waterPlaneHeight * transform.localScale.y);
                    transform.SetScaleY(1);
                }
                CreateWaterPlane(waterPlaneWidth, waterPlaneHeight, waterPlaneWaveWidth, waterPlaneWaveHeight);
            }
        }
        private void CreateWaterfallFoamCall() {
            if (!this || !gameObject || !transform) {
                return;
            }
            if (waterPlaneWidth > 0 && waterPlaneHeight > 0) {
                if (waterfallTopFoam) {
                    var shape = waterfallTopFoam.shape;
                    shape.scale = new Vector3(waterPlaneWidth, 1, 1);
                    shape.position = new Vector3(0, waterPlaneHeight / 2, 0);
                }
                if (waterfallBottomFoam) {
                    var shape = waterfallBottomFoam.shape;
                    shape.scale = new Vector3(waterPlaneWidth, waterPlaneWidth / 5, waterPlaneWidth / 5);
                    shape.position = new Vector3(0, waterPlaneHeight / -2, 0);
                }
            }
        }
        private void CreateWaterPlane(int planeWidth, int planeHeight, int waveWidth, int waveHeight) {
#if UNITY_EDITOR
            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>()) {
                var widthSegments = waveWidth > 0
                    ? planeWidth / waveWidth
                    : 1;
                var heightSegments = waveHeight > 0
                    ? planeHeight / waveHeight
                    : 1;

                //Max segment number is 254, because a mesh can't have more 
                //than 65000 vertices (254^2 = 64516 max. number of vertices)
                widthSegments = Mathf.Clamp(widthSegments, 1, 254);
                heightSegments = Mathf.Clamp(heightSegments, 1, 254);

                //Generate a name for the mesh that will be created
                string planeMeshAssetName = waterPlaneObjectName + widthSegments + "x" + heightSegments
                                            + "W" + planeWidth + "H" + planeHeight + ".asset";

                //Load the mesh from the save location
                Mesh mesh = (Mesh)UnityEditor.AssetDatabase.LoadAssetAtPath(waterPlaneSaveLocation + planeMeshAssetName, typeof(Mesh));

                //If there isn't a mesh located under assets, create the mesh
                if (mesh == null) {
                    mesh = new Mesh();
                    mesh.name = planeMeshAssetName;

                    int hCount2 = widthSegments + 1;
                    int vCount2 = heightSegments + 1;
                    int numTriangles = widthSegments * heightSegments * 6;
                    int numVertices = hCount2 * vCount2;

                    Vector3[] vertices = new Vector3[numVertices];
                    Vector2[] uvs = new Vector2[numVertices];
                    int[] triangles = new int[numTriangles];
                    Vector4[] tangents = new Vector4[numVertices];
                    Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
                    Vector2 anchorOffset = Vector2.zero;

                    int index = 0;
                    float uvFactorX = 1.0f / widthSegments;
                    float uvFactorY = 1.0f / heightSegments;
                    float scaleX = planeWidth / widthSegments;
                    float scaleY = planeHeight / heightSegments;

                    //Generate the vertices
                    for (float y = 0.0f; y < vCount2; y++) {
                        for (float x = 0.0f; x < hCount2; x++) {
                            vertices[index] = new Vector3(x * scaleX - planeWidth / 2f - anchorOffset.x, 0.0f, y * scaleY - planeHeight / 2f - anchorOffset.y);

                            tangents[index] = tangent;
                            uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
                        }
                    }

                    //Reset the index and generate triangles
                    index = 0;
                    for (int y = 0; y < heightSegments; y++) {
                        for (int x = 0; x < widthSegments; x++) {
                            triangles[index] = (y * hCount2) + x;
                            triangles[index + 1] = ((y + 1) * hCount2) + x;
                            triangles[index + 2] = (y * hCount2) + x + 1;

                            triangles[index + 3] = ((y + 1) * hCount2) + x;
                            triangles[index + 4] = ((y + 1) * hCount2) + x + 1;
                            triangles[index + 5] = (y * hCount2) + x + 1;
                            index += 6;
                        }
                    }

                    //Update the mesh properties (vertices, UVs, triangles, normals etc.)
                    mesh.vertices = vertices;
                    mesh.uv = uvs;
                    mesh.triangles = triangles;
                    mesh.tangents = tangents;
                    mesh.RecalculateNormals();

                    //Save the newly created mesh under save location to reload later
                    UnityEditor.AssetDatabase.CreateAsset(mesh, waterPlaneSaveLocation + planeMeshAssetName);
                    UnityEditor.AssetDatabase.SaveAssets();
                }

                //Update mesh
                meshFilter.sharedMesh = mesh;
                mesh.RecalculateBounds();
            }
#endif
        }
    }
}