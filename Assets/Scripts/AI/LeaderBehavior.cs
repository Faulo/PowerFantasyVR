using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBehavior : MonoBehaviour
{
    public float alphaFactor = 0.2f;

    private GameObject player;
    private GameObject[] arrayOfTargets;
    Rigidbody rigidBody;
    private Transform currentTarget;
    private float currentDistance;
    private int num;
    private float nearTarget = 10.0f;

    private Vector3 transformationVector;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve all targets
        arrayOfTargets = GameObject.FindGameObjectsWithTag("LeaderTarget");
        //Retrieve player
        player = GameObject.FindGameObjectWithTag("Player");

        // Set number of Targets
        num = arrayOfTargets.Length - 1;

        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find next target
        if(num < 0)
        {
            num = arrayOfTargets.Length - 1;
        }
        currentTarget = arrayOfTargets[num].transform;

        // Decide if next target is used
        currentDistance = Vector3.Distance(currentTarget.position, transform.position);
        if (currentDistance < nearTarget)
        {
            num--;
        }
        
        // Move leader to target
        transformationVector = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z) - transform.position;
        rigidBody.AddRelativeForce(transformationVector * alphaFactor);
    }
}
