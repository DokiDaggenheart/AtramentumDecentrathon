using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelContactChecker
{
    private readonly Collider _wheelCollider;
    private bool _isGrounded;

    public WheelContactChecker(Collider wheelCollider)
    {
        _wheelCollider = wheelCollider;
    }

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider != null)
        {
            _isGrounded = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }
}


