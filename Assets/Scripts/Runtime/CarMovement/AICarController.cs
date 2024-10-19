using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : CarControllerBase
{
    private Transform _target;

    public void Initialize(CarModel model, Transform target)
    {
        _model = model;
        _target = target;
    }

    public override void OnUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 direction = (_target.position - _model.carBody.position).normalized;
        float steering = Vector3.Cross(_model.carBody.forward, direction).y;

        _model.Turn(steering);
        _model.Accelerate();
    }
}


