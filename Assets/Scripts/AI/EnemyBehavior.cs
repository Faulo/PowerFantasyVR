using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace PFVR.AI
{
    public class EnemyBehavior : MonoBehaviour
    {
        public GameObject player;
        public Transform targetPosition;
        public Vector3 playerPos;

        // Start is called before the first frame update
        void Start()
        {
            Seeker seeker = GetComponent<Seeker>();
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
            playerPos = player.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(playerPos * Time.deltaTime * 0.2f);

        }

        public void OnPathComplete(Path p)
        {
            Debug.Log("Created Path" + p.error);
        }
    }
}
