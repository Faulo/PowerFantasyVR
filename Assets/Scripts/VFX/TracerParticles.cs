using PFVR.OurPhysics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFVR.VFX {
    [RequireComponent(typeof(ParticleSystem))]
    public class TracerParticles : MonoBehaviour {
        [SerializeField, Range(0, 10)]
        private float minimumSpeed = 0;
        [SerializeField]
        private AnimationCurve simulationSpeedOverSpeed = default;
        [SerializeField]
        private AnimationCurve emissionOverSpeed = default;

        private IMotor target;
        private new ParticleSystem particleSystem;
        private ParticleSystem.MainModule particleSystemMain;
        private ParticleSystem.EmissionModule particleSystemEmission;

        // Start is called before the first frame update
        void Start() {
            target = GetComponentInParent<IMotor>();
            particleSystem = GetComponent<ParticleSystem>();
            particleSystemMain = particleSystem.main;
            particleSystemEmission = particleSystem.emission;
        }

        // Update is called once per frame
        void Update() {
            if (target.speed > minimumSpeed) {
                transform.LookAt(target.position + target.velocity);
                particleSystemMain.simulationSpeed = simulationSpeedOverSpeed.Evaluate(target.speed - minimumSpeed);
                particleSystemEmission.rateOverTime = emissionOverSpeed.Evaluate(target.speed - minimumSpeed);
            } else {
                particleSystemMain.simulationSpeed = 0;
                particleSystemEmission.rateOverTime = 0;
                particleSystem.Clear();
            }
        }
    }
}