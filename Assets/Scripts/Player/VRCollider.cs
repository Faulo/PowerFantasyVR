﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

namespace PFVR.Player {
    [RequireComponent(typeof(CapsuleCollider))]
    public class VRCollider : MonoBehaviour {
        [SerializeField]
        private Camera headMountedDisplay = default;
        private new CapsuleCollider collider;

        void Start() {
            collider = GetComponent<CapsuleCollider>();
        }

        void Update() {
            //Set size and position of the capsule collider so it maches our head.
            collider.height = headMountedDisplay.transform.localPosition.y;
            collider.center = new Vector3(headMountedDisplay.transform.localPosition.x, headMountedDisplay.transform.localPosition.y / 2, headMountedDisplay.transform.localPosition.z);
        }
    }
}