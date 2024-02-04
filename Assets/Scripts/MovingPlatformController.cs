using System;
using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatformController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public float speed = 2f;
    
    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    [SerializeField] private bool notPlatform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (waypoints.Count > 0)
            SetNextWaypoint();
        else
            Debug.LogError("No waypoints assigned to the MovingPlatformController.");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
        
        if (transform.position == waypoints[currentWaypointIndex].position)
        {
            SetNextWaypoint();
        }
        
    }

    private void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }
    
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(notPlatform)
            return;
        
        var player = other.gameObject.GetComponent<Player>();
        
        player.gameObject.transform.SetParent(transform, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (notPlatform)
            return;
        
        var player = other.gameObject.GetComponent<Player>();
        
        player.gameObject.transform.SetParent(null);
    }
}