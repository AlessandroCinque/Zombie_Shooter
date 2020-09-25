using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Logic : MonoBehaviour
{
    public GameObject[] spawnPointS;
    public void Spawn(GameObject spawn_me)
    {
        GameObject spawnPoint = GetRandomSpawnPoint();
        spawn_me.transform.position = spawnPoint.transform.position;
    }
    GameObject GetRandomSpawnPoint()
    {
        return spawnPointS[Random.Range(0,spawnPointS.Length)];
    }
}
