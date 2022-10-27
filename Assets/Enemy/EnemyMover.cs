using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    List<Node> path = new List<Node>();


    [SerializeField] [Range(0f, 5f)]float speed = 1f; //set a range so it won't go lower than 0

    Enemy enemy;

    GridManager gridManager;
    Pathfinder pathfinder;


    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();

    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();

        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }


    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinate(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }


    IEnumerator FollowPath()
    {
        for( int i = 1; i < path.Count; i++)
        {

            Vector3 startPostion = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinate(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPostion, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }

        }
        FinishPath();
    }
}
