using Slothsoft.UnityExtensions;
using UnityEngine;
using Valve.VR;

namespace PFVR.Backend {
    /// <summary>
    /// Adds glove and/or tracker materials to any descendant <see cref="SteamVR_RenderModel"/>s.
    /// </summary>
    [ExecuteInEditMode]
    public sealed class PreviewConfig : MonoBehaviour {
        [SerializeField]
        Material gloveMaterial = default;

        [SerializeField]
        Material trackerMaterial = default;

        void OnValidate() {
            if (gloveMaterial != null) {
                GetComponentsInChildren<SkinnedMeshRenderer>()
                    .ForAll(renderer => {
                        renderer.material = gloveMaterial;
                    });
            }

            if (trackerMaterial != null) {
                GetComponentsInChildren<MeshRenderer>()
                    .ForAll(renderer => {
                        renderer.material = trackerMaterial;
                        var model = renderer.GetComponentInParent<SteamVR_RenderModel>();
                        if (model != null) {
                            model.shader = trackerMaterial.shader;
                            model.UpdateModel();
                        }
                    });
            }
        }
    }
}