using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : CarControllerBase
{
    private float _moveInput;
    private float _turnInput;

    public override void Initialize(CarModel model)
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
            _model.Accelerate(_model.maxSpeed);
        }
        else if (_moveInput < 0)
        {
            _model.Brake(true);
        }
        else
        {
            _model.Brake(false);
        }
        _model.Turn(_turnInput * _model.turnSpeed);
    }
}




