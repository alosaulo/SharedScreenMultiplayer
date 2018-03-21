using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectToPool {
    public GameObject PrefabToPool;
    public int MaxSpawns;
    public bool Expand;
    [HideInInspector]
    public int Counter = 0;

}
