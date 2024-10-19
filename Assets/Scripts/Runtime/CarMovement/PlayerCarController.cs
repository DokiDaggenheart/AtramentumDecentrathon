using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : CarControllerBase
{
    private CarModel _model;
    private float _moveInput;
    private float _turnInput;

    public void Initialize(CarModel model)
    {
        _model = model;
    }

    public override void OnUpdate()
    {
        HandleInput();
    }

    public void UpdateInputs(float moveInput, float turnInput)
    {
        _moveInput = moveInput;
        _turnInput = turnInput;
    }

    private void HandleInput()
    {
        if (_moveInput > 0)
        {
            _model.Accelerate();
        }
        else if (_moveInput < 0)
        {
            _model.Brake(); 
        }
        _model.Turn(_turnInput * _model.turnSpeed);
    }
}




