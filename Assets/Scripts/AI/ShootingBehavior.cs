using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;
    public float fireRate;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        bullet = transform.Find("Bullet").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
}
