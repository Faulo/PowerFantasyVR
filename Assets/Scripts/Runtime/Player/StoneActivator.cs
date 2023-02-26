using PFVR.Player;
using PFVR.ScriptableObjects;
using UnityEngine;

public class StoneActivator : MonoBehaviour {
    public Gesture gesture;
    public GameObject stone;

    // Start is called before the first frame update
    void Start() {
        GestureConnector.onGestureUnlock += ActivateStone;
        GestureConnector.onGestureLock += DeactivateStone;
    }

    void ActivateStone(Gesture gesture) {
        if (this.gesture.Equals(gesture)) {
            stone.SetActive(true);
            stone.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", gesture.spellColor);
        }
    }
    void DeactivateStone(Gesture gesture) {
        if (this.gesture.Equals(gesture)) {
            stone.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
