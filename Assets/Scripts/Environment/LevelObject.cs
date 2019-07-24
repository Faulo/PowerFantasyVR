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

        private void Start() {
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
        void EnableRigidbody() {
            var rigidbody = GetComponent<Rigidbody>();
            if (!rigidbody) {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }
            rigidbody.mass = rigidbodyDensity * transform.localScale.x * transform.localScale.y * transform.localScale.z;
            rigidbody.drag = rigidbodyDrag;
        }

        void SetLayer(string layerName) {
            var layerId = LayerMask.NameToLayer(layerName);
            GetComponentsInChildren<Transform>().ForAll(transform => transform.gameObject.layer = layerId);
        }
    }
}