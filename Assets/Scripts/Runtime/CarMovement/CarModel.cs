using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel
{
    public Transform carBody;
    public Transform[] wheels;
    private Rigidbody _rigidBody;
    public readonly float maxSpeed = 50f;
    public readonly float acceleration = 15f;
    public readonly float activeBrakeStrength;
    public readonly float passiveBrakeStrength;
    public readonly float turnSpeed;

    private float _currentSpeed;

    public int currentCheckpointIndex;
    public int totalCheckpoints;
    public int currentLap = 0;
    public bool raceFinished = false;

    public CarModel(Transform body, Transform[] wheels, Rigidbody rb, float maxSpeed, float acceleration, float activeBrakeForce, float passiveBrakeForce, float turnSpeed)
    {
        carBody = body;
        this.wheels = wheels;
        _rigidBody = rb;
        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.activeBrakeStrength = activeBrakeForce;
        this.passiveBrakeStrength = passiveBrakeForce;
        this.turnSpeed = turnSpeed;
    }

    public void Accelerate(float maxSpeed)
    {
        if (_currentSpeed > maxSpeed)
        {
            Brake(false);
            return;
        }
        if (_currentSpeed < maxSpeed)
        {
            _currentSpeed += acceleration * Time.deltaTime;
        }
        Vector3 forward = carBody.forward * _currentSpeed;
        _rigidBody.velocity = new Vector3(forward.x, _rigidBody.velocity.y, forward.z);
        RotateWheels(_currentSpeed);
    }

    public void Brake(bool isBrakeActive)
    {
        float brakeStrength;
        if (isBrakeActive)
            brakeStrength = activeBrakeStrength;
        else
            brakeStrength = passiveBrakeStrength;
        _currentSpeed = Mathf.Max(_currentSpeed - brakeStrength * Time.deltaTime, 0);
        Vector3 forward = carBody.forward * _currentSpeed;
        _rigidBody.velocity = new Vector3(forward.x, _rigidBody.velocity.y, forward.z);
        RotateWheels(_currentSpeed);
    }

    public void Turn(float steeringAngle)
    {
        RotateFrontWheels(steeringAngle);
        carBody.Rotate(Vector3.up, steeringAngle * Time.deltaTime);
        float speedReductionFactor = 1 - Mathf.Abs(steeringAngle) / turnSpeed;
        speedReductionFactor = Mathf.Clamp(speedReductionFactor, 0.8f, 1f);
        Vector3 currentVelocity = _rigidBody.velocity;
        Vector3 forwardVelocity = carBody.forward * Vector3.Dot(currentVelocity, carBody.forward);
        forwardVelocity *= speedReductionFactor;
        _rigidBody.velocity = new Vector3(forwardVelocity.x, currentVelocity.y, forwardVelocity.z);
    }

    private void RotateWheels(float speed)
    {
        foreach (var wheel in wheels)
        {
            wheel.Rotate(Vector3.right, speed * Time.deltaTime * 360);
        }
    }

    private void RotateFrontWheels(float steeringAngle)
    {
        wheels[0].localRotation = Quaternion.Euler(0, steeringAngle, 0);
        wheels[1].localRotation = Quaternion.Euler(0, steeringAngle, 0);
    }

    public void passCheckpoint(int newIndex)
    {
        currentCheckpointIndex = newIndex;
    }
}