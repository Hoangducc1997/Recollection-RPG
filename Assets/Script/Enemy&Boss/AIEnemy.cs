using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIEnemy : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    private void Start()
    {
        path = GetComponent<AIPath>();
    }

    // Update is called once per frame
    private void Update()
    {
        path.maxSpeed = moveSpeed;
        path.destination = target.position;
    }
}
