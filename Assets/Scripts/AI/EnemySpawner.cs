﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemyCount = 1000;
    public Transform prefab;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate all enemies
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}