using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarBinderBase : MonoBehaviour
{
    [SerializeField] protected Transform carBody;
    [SerializeField] protected Transform[] carWheels;
    [SerializeField] protected Collider[] rearWheelColliders;
    [Header("Car Settings")]
    [SerializeField] protected float maxSpeed = 100f;
    [SerializeField] protected float acceleration = 30f;
    [SerializeField] protected float activeBrakeForce = 50f;
    [SerializeField] protected float passiveBrakeForce = 20f;
    [SerializeField] protected float turnSpeed = 5f;

    public virtual void PassCheckpoint(int newIndex)
    {

    }
}
