using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;


    public bool IsPlaceable{get{return isPlaceable;}}

    public bool GetIsPlaceable()
    {
        return isPlaceable;
    }

    void OnMouseDown()
    {
        if(isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position); //If it works, will return true, else false
            isPlaceable = !isPlaced;
        }
    }
}
