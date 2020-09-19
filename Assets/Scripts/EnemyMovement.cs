using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    WaveConfiguration waveConfig = null;
    List<Transform> waypoints = null;
    int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.getWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        if (waypointIndex < waypoints.Count)
        {
            float movementSpeed = waveConfig.getMovementSpeed() * Time.deltaTime;
            Vector3 newPos = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, movementSpeed);
            transform.position = newPos;

            if (newPos == waypoints[waypointIndex].transform.position)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void setWaveConfig(WaveConfiguration waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
