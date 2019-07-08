using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.VFX {
    [ExecuteInEditMode, RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemAutoScale : MonoBehaviour {
        [SerializeField]
        private AstarPath targetPath;

        // Start is called before the first frame update
        void Update() {
            /*
            if (targetPath != null) {
                var system = GetComponent<ParticleSystem>();
                var shape = system.shape;
                shape.shapeType = ParticleSystemShapeType.Box;
                shape.position = targetPath.data.gridGraph.center;
                shape.scale = new Vector3(targetPath.data.gridGraph.width, 100, targetPath.data.gridGraph.depth);
            }
            //*/
        }
    }
}