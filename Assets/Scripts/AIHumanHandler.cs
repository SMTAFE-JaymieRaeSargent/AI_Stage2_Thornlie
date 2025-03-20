using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class AIHumanHandler : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Transform[] _waypoint;
    [SerializeField] Transform _player;
    [SerializeField] float _detectionDistance = 10;
    [SerializeField] int _currentWaypointIndex = 0;
    [SerializeField] float _idleTimer = 0;
    [SerializeField] float _timerLimit = 3;
    [SerializeField] bool _isIdle;
    [SerializeField] bool _canWander = false;
    private void Patrol()
    {
        _agent.SetDestination(_waypoint[_currentWaypointIndex].position);

        if (_agent.remainingDistance <= 0.1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoint.Length)
            {
                _currentWaypointIndex = 0;
            }
            _agent.SetDestination(_waypoint[_currentWaypointIndex].position);

        }
        Debug.Log($"Waypoint {_currentWaypointIndex} : {_agent.destination}");

    }
    void Chase()
    {       
        if (_agent.isStopped)
        {
            _agent.isStopped = false;
        }
        _agent.SetDestination(_player.position);
        Debug.Log($"Player : {_agent.destination}");

    }
    void Idle()//Randomly
    {
        _idleTimer += Time.deltaTime;
        if (_idleTimer >= _timerLimit )
        {
            _isIdle = !_isIdle;
            if (_isIdle)
            {
                //Stop moving and stay put
                _agent.isStopped = true;
                _timerLimit = Random.Range(0.5f, 5);
            }
            else
            {
                _canWander = !_canWander;
                if (_canWander)
                {
                    _agent.SetDestination(GetRandomPosition());
                }
                //Go
                _agent.isStopped = false;
                _timerLimit = Random.Range(5, 15);
            }
            _idleTimer = 0;
        }     

    }
    void Wander()
    {
        //go to a random point on the navmesh 
        //within range...please limit how far
        if (_agent.remainingDistance <= 0.1f)
        {
            _agent.SetDestination(GetRandomPosition());
        }
    }
    Vector3 GetRandomPosition()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomDirection = Random.insideUnitSphere * 10;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection,out hit, 10,1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }
    private void Start()
    {
        _timerLimit = Random.Range(5, 15);

        _agent.SetDestination(_waypoint[_currentWaypointIndex].position);

       
    }
    private void Update()
    {
        if (_detectionDistance >= Vector3.Distance(this.transform.position, _player.position))
        {
            Chase();
        }
        else
        {
            Idle();
            if (_canWander)
            {
                Wander();
            }
            else
            {
                Patrol();
            }
        }
    }
}
