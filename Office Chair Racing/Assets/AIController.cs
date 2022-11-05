using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIController : MonoBehaviour
{
    public Rigidbody rb;

    public Transform targetPosition;

    private Seeker seeker;

    public Path path;

    public float speed;

    public float nextWaypointDistance = 3f;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        seeker = GetComponent<Seeker>();

        seeker.pathCallback += OnPathComplete;

        seeker.StartPath(transform.position, targetPosition.position);
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        InvokeRepeating("Kick", 0, 0.5f);

        speed = 8f;
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Path returned. Error: " + p.error);

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
        {
            return;
        }

        // Check if we are close enough to switch to the next waypoint
        reachedEndOfPath = false;
        bool checkWaypointProximity = true;
        float distanceToWaypoint;
        while (checkWaypointProximity)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                } else
                {
                    reachedEndOfPath = true;
                    checkWaypointProximity = false;
                }
            } else
            {
                checkWaypointProximity = false;
            }
        }

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        rb.rotation = Quaternion.LookRotation(dir);
    }

    public void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete;
    }

    public void Kick()
    {
        Debug.Log("Kick called");
        Debug.Log(path);
        Debug.Log(!reachedEndOfPath);
        if (path != null & !reachedEndOfPath)
        {
            Debug.Log("Conditions met");
            Movement.Kick(speed, rb);
        }
    }
}
