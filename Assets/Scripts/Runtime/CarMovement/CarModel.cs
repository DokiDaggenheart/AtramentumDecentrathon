using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel
{
    public Transform carBody;
    public Transform[] wheels;
    private WheelContactChecker[] rearWheelCheckers;
    private Rigidbody _rigidbody;
    public float maxSpeed = 50f;
    public float acceleration = 15f;
    public float brakeStrength = 30f;
    public float turnSpeed = 10f;

    private float _currentSpeed;

    public CarModel(Transform body, Transform[] wheels, WheelContactChecker[] rearWheelCheckers, Rigidbody rb, float maxSpeed, float acceleration, float brakeForce, float turnSpeed)
    {
        carBody = body;
        this.wheels = wheels;
        this.rearWheelCheckers = rearWheelCheckers;
        _rigidbody = rb;
        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.brakeStrength = brakeForce;
        this.turnSpeed = turnSpeed;
    }

    public void Accelerate()
    {
        Debug.Log(AreRearWheelsOnGround());
        if (AreRearWheelsOnGround()) 
        {
            if (_currentSpeed < maxSpeed)
            {
                _currentSpeed += acceleration * Time.deltaTime;
            }

            Vector3 forward = carBody.forward * _currentSpeed;
            _rigidbody.velocity = new Vector3(forward.x, _rigidbody.velocity.y, forward.z);
            RotateWheels(_currentSpeed);
        }
    }

    public void Brake()
    {
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

    private bool AreRearWheelsOnGround()
    {
        foreach (var checker in rearWheelCheckers)
        {
            if (!checker.IsGrounded)
            {
                return false;
            }
        }
        return true;
    }
}