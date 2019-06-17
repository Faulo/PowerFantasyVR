using PFVR.OurPhysics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.Spells.LaserBolt {
    [RequireComponent(typeof(Rigidbody))]
    public class Bolt : MonoBehaviour {
        public Vector3 velocity {
            get => GetComponent<Rigidbody>().velocity;
            set => GetComponent<Rigidbody>().velocity = value;
        }
    }
}
