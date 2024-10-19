using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RubberBandingSystem : MonoBehaviour
{
    public float rubberBandingDistanceThreshold = 20f;
    public float maxSpeedAdjustment = 10f;

    [Inject] private RaceManager _raceManager; 
    public float maxSpeedWithRubberBanding(CarModel model, PlayerCarBinder playerCarBinder)
    {
        int botCheckpointIndex = model.currentCheckpointIndex;
        int playerCheckpointIndex = _raceManager.playerCheckpointIndex;
        float botProgress = botCheckpointIndex;
        float playerProgress = playerCheckpointIndex;

        float distanceFromPlayer = Vector3.Distance(model.carBody.position, playerCarBinder.GetPosition());

        if (distanceFromPlayer > rubberBandingDistanceThreshold)
        {
            if (botProgress > playerProgress)
            {
                Debug.Log("Замедление");
                return Mathf.Max(model.maxSpeed - maxSpeedAdjustment * Time.deltaTime, model.maxSpeed - maxSpeedAdjustment);
            }
            else if (botProgress < playerProgress)
            {
                Debug.Log("Ускорение");
                return Mathf.Min(model.maxSpeed + maxSpeedAdjustment * Time.deltaTime, model.maxSpeed + maxSpeedAdjustment);
            }
        }
        return model.maxSpeed;
    }
}
