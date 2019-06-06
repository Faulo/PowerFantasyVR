using Slothsoft.UnityExtensions;
using UnityEngine;
using Valve.VR;

namespace PFVR.Debugging {
    [ExecuteInEditMode]
    public class PreviewConfig : MonoBehaviour {
        [SerializeField]
        private Material gloveMaterial = default;

        [SerializeField]
        private Material trackerMaterial = default;

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