using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarControllerBase : IUpdatable
{
    protected CarModel _model;

    public virtual void Initialize(CarModel model)
    {
        _model = model;
    }

    public abstract void OnUpdate();
}

