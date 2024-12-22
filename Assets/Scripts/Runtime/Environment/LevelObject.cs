using PFVR.OurPhysics;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.Environment {
    /// <summary>
    /// Utility class that lets game objects easily switch between their physics behavior.
    /// 
    /// All visible objects in a scene should have this as one of their root components.
    /// </summary>
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
        LevelObjectType type = default;

        [Space]
        [SerializeField, Range(0, 10)]
        float rigidbodyDensity = 1;
        [SerializeField, Range(0, 100)]
        float rigidbodyDrag = 0;
        [Space]
        [SerializeField, Range(0, 1000)]
        int waterPlaneWidth = 0;
        [SerializeField, Range(0, 1000)]
        int waterPlaneHeight = 0;
        [SerializeField, Range(0, 100)]
        int waterPlaneWaveWidth = 1;
        [SerializeField, Range(0, 100)]
        int waterPlaneWaveHeight = 1;
        [SerializeField]
        bool waterPlaneAutoUpdate = false;
        public static string waterPlaneObjectName = "Water";
        public static string waterPlaneSaveLocation = "Assets/Meshes/Environment/Water/";
        float waterPlaneRoutineId;
        [Space]
        [SerializeField]
        ParticleSystem waterfallTopFoam = default;
        [SerializeField]
        ParticleSystem waterfallBottomFoam = default;

        [Space]
        [SerializeField, Range(0, 10)]
        float destroyableHPDensity = 1;
        [SerializeField]
        GameObject destroyableDamageTakenPrefab = default;
        [SerializeField]
        GameObject destroyableDamageHealedPrefab = default;
        [SerializeField]
        GameObject destroyableDeadPrefab = default;

        [Space]
        [SerializeField]
        bool levelOfDetail = false;
        [SerializeField, Range(0, 1)]
        float levelOfDetailCutoff = 0.005f;
        float volume => transform.localScale.x * transform.localScale.y * transform.localScale.z;

        void Start() {
            ApplyType();
        }

        void Update() {
            if (!Application.isPlaying) {
                ApplyType();
            }
        }

        void ApplyType() {
            switch (type) {
                case LevelObjectType.Intangible:
                    SetStatic(true);
                    DisableColliders();
                    DisableRigidbody();
                    DisableLighting();
                    RemoveDestroyable();
                    SetLayer("Ignore Raycast");
                    break;
                case LevelObjectType.StaticCollider:
                    SetStatic(true);
                    EnableColliders();
                    DisableRigidbody();
                    EnableStaticLighting();
                    RemoveDestroyable();
                    SetLayer("Ground");
                    break;
                case LevelObjectType.Rigidbody:
                    SetStatic(false);
                    EnableColliders();
                    EnableGravityRigidbody();
                    EnableDynamicLighting();
                    AddDestroyable();
                    SetLayer("Obstacle");
                    break;
                case LevelObjectType.Tree:
                    SetStatic(true);
                    EnableColliders();
                    DisableRigidbody();
                    EnableStaticLighting();
                    AddDestroyable();
                    SetLayer("Obstacle");
                    break;
                case LevelObjectType.Water:
                    SetStatic(false);
                    DisableColliders();
                    DisableRigidbody();
                    DisableLighting();
                    RemoveDestroyable();
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
                    RemoveDestroyable();
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


        void SetStatic(bool value) {
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.isStatic = value);
        }
        void DisableLighting() {
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = false;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }
        void EnableStaticLighting() {
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                renderer.receiveShadows = true;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }
        void EnableDynamicLighting() {
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                renderer.receiveShadows = true;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }

        void DisableColliders() {
            GetComponentsInChildren<Collider>().ForAll(collider => collider.enabled = false);
        }
        void EnableColliders() {
            GetComponentsInChildren<Collider>().ForAll(collider => collider.enabled = true);
        }

        void DisableRigidbody() {
            var rigidbody = GetComponent<Rigidbody>();
            if (rigidbody) {
                DestroyImmediate(rigidbody);
            }
        }
        void EnableGravityRigidbody() {
            var rigidbody = GetComponent<Rigidbody>();
            if (!rigidbody) {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }
            rigidbody.mass = rigidbodyDensity * volume;
            rigidbody.drag = rigidbodyDrag;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
        void EnableKinematicRigidbody() {
            var rigidbody = GetComponent<Rigidbody>();
            if (!rigidbody) {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }
            rigidbody.mass = rigidbodyDensity * volume;
            rigidbody.drag = rigidbodyDrag;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }

        void SetLayer(string layerName) {
            int layerId = LayerMask.NameToLayer(layerName);
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.layer = layerId);
        }

        void EnableLevelOfDetail() {
            var lodGroup = GetComponent<LODGroup>();
            if (!lodGroup) {
                lodGroup = gameObject.AddComponent<LODGroup>();
            }
            var lods = new LOD[1];
            lods[0].renderers = GetComponentsInChildren<Renderer>();
            lods[0].screenRelativeTransitionHeight = levelOfDetailCutoff;
            lodGroup.SetLODs(lods);
        }
        void DisableLevelOfDetail() {
            var lodGroup = GetComponent<LODGroup>();
            if (lodGroup) {
                DestroyImmediate(lodGroup);
            }
        }


        void AddDestroyable() {
            var destroyable = GetComponent<BasicDestroyable>();
            if (!destroyable) {
                destroyable = gameObject.AddComponent<BasicDestroyable>();
            }
            destroyable.maxHP = destroyableHPDensity * volume;
            destroyable.damageTakenPrefab = destroyableDamageTakenPrefab;
            destroyable.damageHealedPrefab = destroyableDamageHealedPrefab;
            destroyable.deadPrefab = destroyableDeadPrefab;
        }
        void RemoveDestroyable() {
            var destroyable = GetComponent<BasicDestroyable>();
            if (destroyable) {
                DestroyImmediate(destroyable);
            }
        }





        void CreateWaterPlaneCall() {
            if (!this || !gameObject || !transform) {
                return;
            }
            if (waterPlaneWidth > 0 && waterPlaneHeight > 0) {
                if (transform.localScale.x != 1) {
                    waterPlaneWidth = (int)(waterPlaneWidth * transform.localScale.x);
                    transform.localScale = transform.localScale.WithX(1);
                }
                if (transform.localScale.y != 1) {
                    waterPlaneHeight = (int)(waterPlaneHeight * transform.localScale.y);
                    transform.localScale = transform.localScale.WithY(1);
                }
                CreateWaterPlane(waterPlaneWidth, waterPlaneHeight, waterPlaneWaveWidth, waterPlaneWaveHeight);
            }
        }
        void CreateWaterfallFoamCall() {
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
        void CreateWaterPlane(int planeWidth, int planeHeight, int waveWidth, int waveHeight) {
#if UNITY_EDITOR
            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>()) {
                int widthSegments = waveWidth > 0
                    ? planeWidth / waveWidth
                    : 1;
                int heightSegments = waveHeight > 0
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
                var mesh = (Mesh)UnityEditor.AssetDatabase.LoadAssetAtPath(waterPlaneSaveLocation + planeMeshAssetName, typeof(Mesh));

                //If there isn't a mesh located under assets, create the mesh
                if (mesh == null) {
                    mesh = new Mesh {
                        name = planeMeshAssetName
                    };

                    int hCount2 = widthSegments + 1;
                    int vCount2 = heightSegments + 1;
                    int numTriangles = widthSegments * heightSegments * 6;
                    int numVertices = hCount2 * vCount2;

                    var vertices = new Vector3[numVertices];
                    var uvs = new Vector2[numVertices];
                    int[] triangles = new int[numTriangles];
                    var tangents = new Vector4[numVertices];
                    var tangent = new Vector4(1f, 0f, 0f, -1f);
                    var anchorOffset = Vector2.zero;

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