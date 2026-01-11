using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<GameObject, Stack<GameObject>> pools = new();

    public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (!pools.ContainsKey(prefab) || pools[prefab].Count == 0)
        {
            return Object.Instantiate(prefab, pos, rot);
        }
        var go = pools[prefab].Pop();
        go.transform.SetPositionAndRotation(pos, rot);
        go.SetActive(true);
        return go;
    }

    public static void Despawn(GameObject go)
    {
        var prefab = go; // assume original prefab identity is handled elsewhere or store link on instance
        go.SetActive(false);
        // For a robust pool, store prefab reference on a Poolable component to map back.
        // Here we simply destroy to keep snippet short:
        Object.Destroy(go);
    }
}
