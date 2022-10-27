using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;


    public bool IsPlaceable{get{return isPlaceable;}}

    GridManager gridManager;
    Pathfinder pathfinder;

    Vector2Int coordinates = new Vector2Int();


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }
    public bool GetIsPlaceable()
    {
        return isPlaceable;
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates= gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates) )
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position); //If it works, will return true, else false
            if(isSuccessful)
            {
            gridManager.BlockNode(coordinates);
            pathfinder.NotifyReceivers();
            }
        }
    }
}
