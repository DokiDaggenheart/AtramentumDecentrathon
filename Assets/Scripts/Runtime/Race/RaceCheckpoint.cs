using UnityEngine;
using Zenject;
public class RaceCheckpoint : MonoBehaviour
{
    [Inject] private RaceManager _raceManager;
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        var car = other.GetComponentInParent<CarBinderBase>();
        if ( car != null)
        {
            Debug.Log(other.name);
            car.PassCheckpoint(checkpointIndex);
            _raceManager.CheckVictory(checkpointIndex);
        }
    }
}
