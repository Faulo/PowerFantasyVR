using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed;
    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;

        Collider collider= gameObject.GetComponent<Collider>();
        collider.enabled = true;
        AstarPath.active.UpdateGraphs(collider.bounds);
    }

    // Update is called once per frame
    void Update()
    {
        Collider collider = gameObject.GetComponent<Collider>();
        collider.enabled = true;
        AstarPath.active.UpdateGraphs(collider.bounds);
    }
}
