using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;

    public Transform spawnLocation;

    public Quaternion spawnRotation;

    public DetectionZone detectionZone;

    public float spawnTime = 0.5f;

    private float timeSinceSpawned = 0.5f;
   

    // Update is called once per frame
    void Update()
    {
        if (detectionZone.detectedOjs.Count > 0)
        {
            timeSinceSpawned += Time.deltaTime;

            if (timeSinceSpawned >= spawnTime)
            {
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                timeSinceSpawned = 0;
            }
        }
        else
        {
            timeSinceSpawned = 0.5f;
        }  
    }
}
