using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 2.5f;
    public bool CreateTower(Tower tower, Vector3 position)
    {

        void Start()
        {
            StartCoroutine(Build());
        }

        Bank bank = FindObjectOfType<Bank>();

        if(bank == null){return false;}



        if (bank.CurrentBalance >= cost)
        {
        Instantiate(tower.gameObject, position, Quaternion.identity);
        bank.Withdraw(cost);
        return true;

        }

        return false;
    }

    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }

}
