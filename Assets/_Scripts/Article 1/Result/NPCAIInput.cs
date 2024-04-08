using System.Collections;
using System.Collections.Generic;
using Tips.Part_1_Result;
using UnityEngine;
using UnityEngineInternal;

public class NPCAIInput : MonoBehaviour, IAgentMovementInput
{
    [SerializeField]
    private Transform[] _waypoints;
    [SerializeField,Min(0.5f)]
    private float _distanceThreshold = 1.0f;
    private int _currentWaypointIndex = 0;

    public Vector2 MovementInput { get; private set; }
    public bool SprintInput { get; private set; }

    private void Update()
    {
        if (_waypoints.Length <= 0)
            return;

        float distance 
            = Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position);
        if (distance < _distanceThreshold)
        {
            _currentWaypointIndex++;
            _currentWaypointIndex
                = _currentWaypointIndex >= _waypoints.Length ? 0 : _currentWaypointIndex;
        }
        Vector3 currentWaypoint = _waypoints[_currentWaypointIndex].position;
        currentWaypoint.y = transform.position.y;
        Vector3 movementDirection = (currentWaypoint - transform.position).normalized;
        MovementInput = new Vector2(movementDirection.x, movementDirection.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawRay(transform.position,new Vector3(MovementInput.x, transform.position.y, MovementInput.y));
    }
}
