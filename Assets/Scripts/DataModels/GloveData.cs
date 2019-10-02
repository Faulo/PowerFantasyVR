using ManusVR.Core.Apollo;
using System;
using UnityEngine;

namespace PFVR.DataModels {
    /// <summary>
    /// A model for ManusVR glove data. Can be converted to <see cref="GestureModel"/>.
    /// </summary>
    [Serializable]
    public class GloveData {
        public device_type_t device;
        public GloveLaterality laterality;
        public double pinkyProximal;
        public double pinkyMedial;
        public double ringProximal;
        public double ringMedial;
        public double middleProximal;
        public double middleMedial;
        public double indexProximal;
        public double indexMedial;
        public double thumbProximal;
        public double thumbMedial;
        public Quaternion wrist;
        public Transform tracker;

        public GestureModel ToGestureModel() {
            return new GestureModel {
                pinkyProximal = (float)pinkyProximal,
                pinkyMedial = (float)pinkyMedial,
                ringProximal = (float)ringProximal,
                ringMedial = (float)ringMedial,
                middleProximal = (float)middleProximal,
                middleMedial = (float)middleMedial,
                indexProximal = (float)indexProximal,
                indexMedial = (float)indexMedial,
                thumbProximal = (float)thumbProximal,
                thumbMedial = (float)thumbMedial,
            };
        }
    }
}