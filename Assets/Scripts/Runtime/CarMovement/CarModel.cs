using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel
{
    public Transform carBody;
    public Transform[] wheels;
    private Rigidbody _rigidbody;
    public float maxSpeed = 50f;
    public float acceleration = 15f;
    public float activeBrakeStrength = 30f;
    public float passiveBrakeStrength = 30f;
    public float turnSpeed = 10f;

    private float _currentSpeed;

    public CarModel(Transform body, Transform[] wheels, Rigidbody rb, float maxSpeed, float acceleration, float activeBrakeForce, float passiveBrakeForce, float turnSpeed)
    {
        carBody = body;
        this.wheels = wheels;
        _rigidbody = rb;
        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.activeBrakeStrength = activeBrakeForce;
        this.passiveBrakeStrength = passiveBrakeForce;
        this.turnSpeed = turnSpeed;
    }

    public void Accelerate()
    {
        if (_currentSpeed < maxSpeed)
        {
            _currentSpeed += acceleration * Time.deltaTime;
        }

        Vector3 forward = carBody.forward * _currentSpeed;
        _rigidbody.velocity = new Vector3(forward.x, _rigidbody.velocity.y, forward.z);
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
        _rigidbody.velocity = new Vector3(forward.x, _rigidbody.velocity.y, forward.z);
        RotateWheels(_currentSpeed);
    }

    public void Turn(float steeringAngle)
    {
        RotateFrontWheels(steeringAngle);
        carBody.Rotate(Vector3.up, steeringAngle * Time.deltaTime);
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

}