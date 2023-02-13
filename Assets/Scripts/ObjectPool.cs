using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool objectpool;
    public GameObject prefabToPool;
    public int poolSize = 10;
    [SerializeField] private float disableTime = 3f;

    private GameObject[] pool;
    private int currentIndex = 0;

    private void Awake()
    {
        if (objectpool == null)
            objectpool = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(prefabToPool, transform);
            pool[i].SetActive(false);
        }
    }

    public GameObject GetObjectFromPool()
    {
        GameObject objectToReturn = pool[currentIndex];
        currentIndex++;
        if (currentIndex >= poolSize)
        {
            currentIndex = 0;
        }
        objectToReturn.SetActive(true);

        StartCoroutine(disableObject(objectToReturn));
        return objectToReturn;
    }

    IEnumerator disableObject(GameObject go)
    {
        yield return new WaitForSeconds(disableTime);
        go.SetActive(false);
    }
}
