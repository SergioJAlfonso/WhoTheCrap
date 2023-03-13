using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPoint : MonoBehaviour
{
    public Transform spawnpointEmpty;

    private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Awake()
    {
        int chCount = spawnpointEmpty.childCount;

        spawnPoints = new Transform[chCount];

        for (int i = 0; i < chCount; i++)
        {
            spawnPoints[i] = spawnpointEmpty.GetChild(i).transform;
        }

        transform.position = spawnPoints[Random.Range(0, chCount)].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
