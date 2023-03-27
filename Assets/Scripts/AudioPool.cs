using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> pooldictionary;

    public static AudioPool instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of audiomanger found.");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    void Start()
    {
        pooldictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab, transform);
                go.name = pool.tag + " : AudioSource(" + i + ")";
                go.SetActive(false);
                objectpool.Enqueue(go);
            }

            pooldictionary.Add(pool.tag, objectpool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rotation)
    {
        if (!pooldictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool Tag doesnt exists");
            return null;
        }

        GameObject go = pooldictionary[tag].Dequeue();
        go.SetActive(true);
        go.transform.position = pos;
        go.transform.rotation = rotation;


        pooldictionary[tag].Enqueue(go);
        return go;
    }
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}
