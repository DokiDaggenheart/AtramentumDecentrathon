using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelContactChecker
{
    private readonly Collider _wheelCollider;
    private readonly float _raycastLength;
    private bool _isGrounded;

    public WheelContactChecker(Collider wheelCollider, float raycastLength)
    {
        _wheelCollider = wheelCollider;
        _raycastLength = raycastLength;
    }

    public bool IsGrounded
    {
        get
        {
            CheckContact();
            return _isGrounded;
        }
    }

    private void CheckContact()
    {
        Ray ray = new Ray(_wheelCollider.bounds.center, Vector3.down);
        _isGrounded = Physics.Raycast(ray, _raycastLength);
    }
}

