using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManusConnector : MonoBehaviour {
    private int playerId;
    private bool isFireballing {
        set {
            FindObjectOfType<Light>().intensity = value ? 0 : 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerId == 0) {
            HandDataManager.GetPlayerNumber(out playerId);
        }
        if (playerId != 0) {
            if (HandDataManager.CanGetHandData(playerId, device_type_t.GLOVE_LEFT)) {
                ApolloHandData apolloHandData = HandDataManager.GetHandData(playerId, device_type_t.GLOVE_LEFT);
                var value = apolloHandData.fingers[(int)ApolloHandData.FingerName.Middle].flexSensorRaw[(int)ApolloHandData.FlexSensorSegment.Medial];
                if (value > 0.9) {
                    Debug.Log("FIRE!");
                    isFireballing = true;
                    Apollo.rumble(GloveLaterality.GLOVE_LEFT, (ushort) (1000 * Time.deltaTime), ushort.MaxValue);
                } else {
                    isFireballing = false;
                }
            }
        }
    }
}
