using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLauncher : MonoBehaviour
{
    public GameObject projectile;

    public Transform spawnLocation;

    public Quaternion spawnRotation;

    public float spawnTime = 0.5f;

    private float timeSinceSpawned = 0.5f;


    // Update is called once per frame
    void Update()
    {

        timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned >= spawnTime)
        {
            Instantiate(projectile, spawnLocation.position, spawnRotation);
            timeSinceSpawned = 0;
        }
    }
}
