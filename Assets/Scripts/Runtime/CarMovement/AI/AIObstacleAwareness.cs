using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObstacleAwareness
{
    public static bool ObstacleOnRaycast(Vector3 startPoint, Vector3 direction, float raycastLength, LayerMask mask)
    {
        if (Physics.Raycast(startPoint, direction, raycastLength, mask))
        {
            Debug.Log("true");
            return true;
        }
        return false;
    }
}
