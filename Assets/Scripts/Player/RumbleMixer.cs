using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ManusVR.Core.Apollo;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace PFVR.Player {
    public class RumbleMixer {
        private GloveLaterality side;
        private ushort interval;
        private bool mix;
        private float[] rumbles = new float[100];
        private int rumbleIndex;
        private IEnumerable<int> rumbleIndexes {
            get {
                for (int i = 0; i < rumbles.Length; i++) {
                    yield return (i + rumbleIndex) % rumbles.Length;
                }
            }
        }

        public RumbleMixer(GloveLaterality side, ushort interval, bool mix) {
            this.side = side;
            this.interval = interval;
            this.mix = mix;
        }

        public IEnumerator RumbleMixerRoutine() {
            if (mix) {
                var wait = new WaitForSecondsRealtime((float)interval / 1000);
                while (true) {
                    var power = Mathf.Clamp01(rumbles[rumbleIndex]);
                    if (power > 0) {
                        Apollo.rumble(side, interval, (ushort)(ushort.MaxValue * power));
                        rumbles[rumbleIndex] = 0;
                    }
                    rumbleIndex = (rumbleIndex + 1) % rumbles.Length;
                    yield return wait;
                }
            }
        }

        public void Rumble(ushort duration, float power) {
            if (mix) {
                foreach (int i in rumbleIndexes) {
                    if (duration > interval) {
                        duration -= interval;
                        rumbles[i] += Mathf.Clamp01(power);
                    } else {
                        rumbles[i] += Mathf.Clamp01(power * duration / interval);
                        break;
                    }
                }
            } else {
                Apollo.rumble(side, duration, (ushort)(ushort.MaxValue * power));
            }
        }
    }
}