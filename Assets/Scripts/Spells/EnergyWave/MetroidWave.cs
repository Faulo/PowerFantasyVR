using UnityEngine;

namespace PFVR.Spells.EnergyWave {
    public class MetroidWave : Wave {
        [Space]
        public ParticleSystem cannonParticleShooter;
        public ParticleSystem chargingParticle;
        public ParticleSystem chargedParticle;
        public ParticleSystem lineParticles;
        public ParticleSystem chargedCannonParticle;
        public ParticleSystem chargedEmission;
        public ParticleSystem muzzleFlash;

        public bool activateCharge;
        public bool charging;
        public bool charged;
        public float holdTime = 1;
        public float chargeTime = .5f;

        float holdTimer;
        float chargeTimer;

        [Space]

        public float punchStrenght = .2f;
        public int punchVibrato = 5;
        public float punchDuration = .3f;
        [Range(0, 1)]
        public float punchElasticity = .5f;

        [Space]
        [ColorUsageAttribute(true, true)]
        public Color normalEmissionColor;
        [ColorUsageAttribute(true, true)]
        public Color finalEmissionColor;

        /*
        void Start() {
        }

        void Update() {

            //SHOOT
            if (Input.GetMouseButtonDown(0)) {
                Shoot();

                holdTimer = Time.time;
                activateCharge = true;
            }

            //RELEASE
            if (Input.GetMouseButtonUp(0)) {
                activateCharge = false;

                if (charging) {
                    chargedCannonParticle.Play();
                    charging = false;
                    charged = false;
                    chargedParticle.transform.DOScale(0, .05f).OnComplete(() => chargedParticle.Clear());
                    lineParticles.Stop();
                }
            }

            //HOLD CHARGE
            if (activateCharge && !charging) {
                if (Time.time - holdTimer > holdTime) {
                    charging = true;
                    chargingParticle.Play();
                    lineParticles.Play();
                    chargeTimer = Time.time;
                }
            }

            //CHARGING
            if (charging && !charged) {
                if (Time.time - chargeTimer > chargeTime) {
                    charged = true;
                    chargedParticle.Play();
                    chargedParticle.transform.localScale = Vector3.zero;
                    chargedParticle.transform.DOScale(1, .4f).SetEase(Ease.OutBack);
                    chargedEmission.Play();
                }
            }

        }


        void Shoot() {
            muzzleFlash.Play();
            cannonParticleShooter.Play();
        }
        //*/
    }
}