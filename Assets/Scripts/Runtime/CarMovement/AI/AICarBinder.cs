using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class AICarBinder : CarBinderBase
{
    [Header("Sensor")]
    [SerializeField] private float sensorFrontOffset;
    [SerializeField] private float sensorSideOffset;
    [SerializeField] private float sensorEndShift;
    [SerializeField] private float sensorLength;
    [SerializeField] private float distanceThreshold = 10;
    public LayerMask ignoringLayerMask;

    private AICarController _carController;
    [Inject]
    public void Construct(AICarController carController)
    {
        _carController = carController;
    }

    private void Awake()
    {
        Rigidbody rb = carBody.GetComponent<Rigidbody>();

        CarModel model = new CarModel(carBody, carWheels, rb, maxSpeed, acceleration, activeBrakeForce, passiveBrakeForce, turnSpeed);
        _carController.Initialize(model);
    }

    private void FixedUpdate()
    {
        _carController.OnUpdate();
        _carController.SetParams(sensorFrontOffset, sensorSideOffset, sensorEndShift, sensorLength, distanceThreshold, ignoringLayerMask);
    }

    public override void PassCheckpoint(int newIndex)
    {
        _carController.OnCheckpointPassed(newIndex); 
    }
}


