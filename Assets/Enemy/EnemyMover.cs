using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField]List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f, 5f)]float speed = 1f; //set a range so it won't go lower than 0

    Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();
       
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Tile Tile = child.GetComponent<Tile>();

            if (Tile != null)
            {
                path.Add(Tile);
            }
        }
        
    }


    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }


    IEnumerator FollowPath()
    {
        foreach(Tile Tile in path)
        {

            Vector3 startPostion = transform.position;
            Vector3 endPosition = Tile.transform.position;
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
