using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

    public static ObjectPooling _instance;

    public List<ObjectToPool> GameObjectsToPool;

    public List<GameObject> PoolOfGameObjects;


    private void Awake()
    {
        _instance = this;
    }

    public GameObject GetPooledObject(string tag) {
        ObjectToPool objectToPool = null;
        for (int i = 0; i < GameObjectsToPool.Count; i++)
        {
            if (GameObjectsToPool[i].PrefabToPool.tag == tag) {
                objectToPool = GameObjectsToPool[i];
            }
        }
        if (objectToPool == null) {
            return null;
        }
        for (int i = 0; i < PoolOfGameObjects.Count; i++)
        {
            if (PoolOfGameObjects[i].tag == tag && 
                PoolOfGameObjects[i].activeSelf == false) {
                return PoolOfGameObjects[i];
            }
        }
        if (objectToPool.Counter < objectToPool.MaxSpawns)
        {
            return InstantiateGameObject(objectToPool);
        }
        else {
            if (objectToPool.Expand == true)
            {
                return InstantiateGameObject(objectToPool);
            }
        }
        return null;
    }

    private GameObject InstantiateGameObject(ObjectToPool objectToPool) {
        GameObject go = Instantiate(objectToPool.PrefabToPool);
        PoolOfGameObjects.Add(go);
        objectToPool.Counter++;
        return go;
    }

}
