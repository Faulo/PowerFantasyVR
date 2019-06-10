using PFVR.OurPhysics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.LaserBolt {
    [RequireComponent(typeof(KinematicRigidbody))]
    public class Bolt : MonoBehaviour {
        public Vector3 velocity {
            get => GetComponent<KinematicRigidbody>().velocity;
            set => GetComponent<KinematicRigidbody>().velocity = value;
        }
    }
}
