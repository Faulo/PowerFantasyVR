using PFVR.OurPhysics;
using UnityEngine;

namespace PFVR.VFX {
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class TracerParticles : MonoBehaviour {
        [SerializeField, Range(0, 10)]
        float minimumSpeed = 0;
        [SerializeField]
        AnimationCurve simulationSpeedOverSpeed = default;
        [SerializeField]
        AnimationCurve emissionOverSpeed = default;

        IMotor target;
        new ParticleSystem particleSystem;
        ParticleSystem.MainModule particleSystemMain;
        ParticleSystem.EmissionModule particleSystemEmission;

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