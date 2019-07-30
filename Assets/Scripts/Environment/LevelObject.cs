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
            Tree
        }
        [SerializeField]
        private LevelObjectType type = default;

        [Space]
        [SerializeField, Range(0, 100)]
        private float rigidbodyDensity = 1;
        [SerializeField, Range(0, 100)]
        private float rigidbodyDrag = 0;
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
                    DisableColliders();
                    DisableRigidbody();
                    DisableLighting();
                    SetLayer("Ignore Raycast");
                    break;
                case LevelObjectType.StaticCollider:
                    EnableColliders();
                    DisableRigidbody();
                    EnableStaticLighting();
                    SetLayer("Ground");
                    break;
                case LevelObjectType.Rigidbody:
                    EnableColliders();
                    EnableRigidbody();
                    EnableDynamicLighting();
                    SetLayer("Obstacle");
                    break;
                case LevelObjectType.Tree:
                    EnableColliders();
                    DisableRigidbody();
                    EnableStaticLighting();
                    SetLayer("Obstacle");
                    break;
            }
            if (levelOfDetail) {
                EnableLevelOfDetail();
            } else {
                DisableLevelOfDetail();
            }
        }

        private void DisableLighting() {
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.isStatic = true);
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = false;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }
        private void EnableStaticLighting() {
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.isStatic = true);
            GetComponentsInChildren<Renderer>().ForAll(renderer => {
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                renderer.receiveShadows = true;
                renderer.allowOcclusionWhenDynamic = true;
            });
        }
        private void EnableDynamicLighting() {
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.isStatic = false);
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
    }
}