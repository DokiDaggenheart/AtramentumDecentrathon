using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class AICarController : CarControllerBase
{
    [Inject] private RaceManager _raceManager;
    [Inject] private PlayerCarBinder _playerCarBinder;
    [Inject] private RubberBandingSystem _rubberBanding;
    private float _sensorFrontOffset;
    private float _sensorSideOffset;
    private float _sensorEndShift;
    private float _sensorLength;
    private float _distanceThreshold;
    private LayerMask _layerMask;
    private Transform _currentTargetWaypoint;
    private int _currentCheckpointIndex = 0;
    private int _currentWaypointIndex = 0;
    public override void OnUpdate()
    {
        MoveTowardsWaypoint();
    }
    
    private void MoveTowardsWaypoint()
    {
        _currentTargetWaypoint = _raceManager.BotCheckpoints[_currentWaypointIndex];
        Vector3 target = new Vector3(_currentTargetWaypoint.position.x, _model.carBody.position.y, _currentTargetWaypoint.position.z);
        Vector3 vectorToTarget = _model.carBody.InverseTransformPoint(target);
        float distanceToTarget = vectorToTarget.magnitude;
        CheckForNextCheckpoint(distanceToTarget);
        float steer = vectorToTarget.x / distanceToTarget;
        
        if (AIObstacleAwareness.ObstacleOnRaycast(GetSensorStart(0f), GetSensorDirection(0f), _sensorLength, _layerMask))
            _model.Brake(true);
        else
            _model.Accelerate(_rubberBanding.maxSpeedWithRubberBanding(_model, _playerCarBinder));

        if (AIObstacleAwareness.ObstacleOnRaycast(GetSensorStart(1f), GetSensorDirection(1f), _sensorLength, _layerMask))
            steer = -0.5f;
        if (AIObstacleAwareness.ObstacleOnRaycast(GetSensorStart(-1f), GetSensorDirection(-1f), _sensorLength, _layerMask))
            steer = 0.5f;
        if (AIObstacleAwareness.ObstacleOnRaycast(GetSensorStart(-1f), GetSensorDirection(0f), _sensorLength, _layerMask))
            steer = 1;
        if (AIObstacleAwareness.ObstacleOnRaycast(GetSensorStart(1f), GetSensorDirection(0f), _sensorLength, _layerMask))
            steer = -1;

        _model.Turn(steer * _model.turnSpeed);
    }

    private void CheckForNextCheckpoint(float distance)
    {
        if (distance <= _distanceThreshold)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _raceManager.BotCheckpoints.Count)
            {
                _currentWaypointIndex = 0;
            }
        }
    }

    public void SetParams(float sensorFrontOffset, float sensorSideOffset, float sensorEndShift, float sensorLength, float distanceThreshold, LayerMask mask)
    {
        _sensorFrontOffset = sensorFrontOffset;
        _sensorSideOffset = sensorSideOffset;
        _sensorEndShift = sensorEndShift;
        _sensorLength = sensorLength;
        _distanceThreshold = distanceThreshold;
        _layerMask = mask;
    }

    private Vector3 GetSensorStart(float direction)
    {
        Transform transform = _model.carBody.transform;
        Vector3 result = transform.position + transform.forward * _sensorFrontOffset + transform.right * _sensorSideOffset * direction;
        result = new Vector3(result.x, result.y + 0.5f, result.z);
        return result;
    }

    private Vector3 GetSensorDirection(float shift)
    {
        Transform transform = _model.carBody.transform;
        Vector3 result = transform.forward * _sensorLength + transform.right * shift * _sensorEndShift;
        result = new Vector3(result.x, result.y + 0.5f, result.z);
        return result;
    }
}