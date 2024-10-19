using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private List<RaceCheckpoint> _raceCheckpoints = new List<RaceCheckpoint>();
    [SerializeField] private List<Transform> _botCheckpoints = new List<Transform>();
    public List<RaceCheckpoint> RaceCheckpoints { get { return _raceCheckpoints;}}
    public List<Transform> BotCheckpoints { get { return _botCheckpoints; } }
    public Transform finishLine;
    public GameObject victoryScreen;
    public int totalLaps = 1; 
    public int playerCheckpointIndex = 0;

    public void CheckVictory(int index)
    {
        Debug.Log(index);
        if (index == _raceCheckpoints.Count - 1)
            FinishRace();
    }
    private void FinishRace()
    {
        Time.timeScale = 0; 
        victoryScreen.SetActive(true);
    }

    private int GetTotalCheckpoints()
    {
        return FindObjectsOfType<RaceCheckpoint>().Length;
    }
}

