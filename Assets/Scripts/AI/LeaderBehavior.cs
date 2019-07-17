using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBehavior : MonoBehaviour
{
    public float alphaFactor = 0.2f;
    public float attackPlayerDistance = 500.0f;
    public float ankorThreshold = 1000.0f;

    private GameObject player;
    private GameObject ankor;
    private GameObject[] arrayOfTargets;
    Rigidbody rigidBody;
    private Transform currentTarget;
    private float currentDistance;
    private int num;
    private float nearTarget = 10.0f;
    private float playerDistance;
    private float ankorDistance;
    private bool lookForPlayer = false;
    private bool normalize = false;

    private Vector3 transformationVector;
    

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve all targets
        arrayOfTargets = GameObject.FindGameObjectsWithTag("LeaderTarget");
        //Debug.Log("Array of Targets: "+arrayOfTargets.Length);
        //Retrieve player
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log("Player exists: " + !player.Equals(null));
        //Retrieve ankor
        ankor = GameObject.FindGameObjectWithTag("Ankor");
        //Debug.Log("Array of Ankor: " + !ankor.Equals(null));
        // Set number of Targets
        num = arrayOfTargets.Length - 1;
        lookForPlayer = true;

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

        currentDistance = Vector3.Distance(currentTarget.position, transform.position);
        if (currentDistance < nearTarget)
        {
            //Debug.Log("Set new Target");
            num--;
            normalize = false;
            lookForPlayer = true;
        }
        // Find player nearby
        if (player != null) { 
            playerDistance = Vector3.Distance(player.transform.position, transform.position);
            ankorDistance = Vector3.Distance(ankor.transform.position, transform.position);
            if (playerDistance < attackPlayerDistance && ankorDistance < ankorThreshold && lookForPlayer)
            {
                currentTarget = player.transform;
                //Debug.Log("Send to player");

            }else if(ankorDistance > ankorThreshold)
            {
                //Debug.Log("Too far away");
                lookForPlayer = false;
                normalize = true;
            }
        }

        // Move leader to target
        transformationVector = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z) - transform.position;
        if(normalize)
        {
            //Debug.Log("Normalize Vector");
            Vector3.Normalize(transformationVector);
            transformationVector = transformationVector * 0.4f;
        }
        rigidBody.AddRelativeForce(transformationVector * alphaFactor);
    }
}
