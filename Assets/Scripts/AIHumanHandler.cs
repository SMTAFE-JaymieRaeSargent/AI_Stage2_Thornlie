using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class AIHumanHandler : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Transform[] _waypoint;
    [SerializeField] Transform _player;
    [SerializeField] float _detectionDistance = 10;
    [SerializeField] int currentWaypointIndex = 0;
    private void Patrol()
    {

        if (_agent.remainingDistance <= 1)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= _waypoint.Length)
            {
                currentWaypointIndex = 0;
            }
            _agent.SetDestination(_waypoint[currentWaypointIndex].position);

        }
    }
    void Chase()
    {       
            _agent.SetDestination(_player.position);        
    }
    private void Start()
    {
        _agent.SetDestination(_waypoint[currentWaypointIndex].position);
    }
    private void Update()
    {
        if (_detectionDistance >= Vector3.Distance(this.transform.position, _player.position))
        {
            Chase();
            Debug.Log($"Player : {_agent.destination}");
        }
        else
        {
            Patrol();
            Debug.Log($"Waypoint {currentWaypointIndex} : {_agent.destination}");

        }
    }
}
