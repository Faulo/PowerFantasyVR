using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBehavior : MonoBehaviour
{
    public float alphaFactor = 0.2f;
    private float alphaFactorUsed;
    public float attackPlayerDistance = 500.0f;
    public float ankorThreshold = 1000.0f;

    private GameObject player;
    private GameObject ankor;
    private GameObject[] arrayOfTargets;
    
    private Transform currentTarget;
    private float currentDistance;
    private int num;
    private float nearTarget = 10.0f;
    private float playerDistance;
    private float ankorDistance;
    private bool lookForPlayer = false;
    private bool normalize = false;
    private Vector3 transformationVector;

    Rigidbody rigidBody;
    public bool chasePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve all targets
        arrayOfTargets = GameObject.FindGameObjectsWithTag("LeaderTarget");
        //Retrieve player
        player = GameObject.FindGameObjectWithTag("Player");
        //Retrieve ankor
        ankor = GameObject.FindGameObjectWithTag("Ankor");
        // Set number of Targets
        num = arrayOfTargets.Length - 1;
        lookForPlayer = true;

        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        alphaFactorUsed = alphaFactor;
        // Find next target
        if (num < 0)
        {
            num = arrayOfTargets.Length - 1;
        }
        currentTarget = arrayOfTargets[num].transform;

        currentDistance = Vector3.Distance(currentTarget.position, transform.position);
        if (currentDistance < nearTarget)
        {
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
                chasePlayer = true;
                alphaFactorUsed = alphaFactor + 2.0f;

            }
            else if(ankorDistance > ankorThreshold)
            {
                lookForPlayer = false;
                chasePlayer = false;
                alphaFactorUsed = alphaFactor;
                normalize = true;
            }
        }

        // Move leader to target
        transformationVector = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z) - transform.position;
        if(normalize)
        {
            Vector3.Normalize(transformationVector);
            transformationVector = transformationVector * 0.4f;
        }
        rigidBody.AddRelativeForce(transformationVector * alphaFactorUsed);
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
