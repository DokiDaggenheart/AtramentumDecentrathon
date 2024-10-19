using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class PlayerCarBinder : CarBinderBase
{
    private PlayerCarController _carController;
    private RaceManager _raceManager;

    [Inject]
    public void Construct(PlayerCarController carController, RaceManager raceManager)
    {
        _carController = carController;
        _raceManager = raceManager;
    }

    private void Awake()
    {
        Rigidbody rb = carBody.GetComponent<Rigidbody>();

        CarModel model = new CarModel(carBody, carWheels, rb, maxSpeed, acceleration, activeBrakeForce, passiveBrakeForce, turnSpeed);
        _carController.Initialize(model);
    }

    private void FixedUpdate()
    {
        HandleInput();
         _carController.OnUpdate();
    }
    
    private void HandleInput()
    {
        float moveInput = 0f;
        float turnInput = 0f;

        if (Input.GetKey(KeyCode.W))
            moveInput = 1f; 
        else if (Input.GetKey(KeyCode.S))
            moveInput = -1f; 
        if (Input.GetKey(KeyCode.A))
            turnInput = -1f; 
        else if (Input.GetKey(KeyCode.D))
            turnInput = 1f; 

        _carController.UpdateInputs(moveInput, turnInput);
    }

    public Vector3 GetPosition()
    {
        return carBody.position;
    }
    public override void PassCheckpoint(int newIndex)
    {
        _carController.OnCheckpointPassed(newIndex);
        _raceManager.playerCheckpointIndex = newIndex;
    }
}
