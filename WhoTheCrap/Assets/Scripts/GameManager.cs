using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    private float maxLow = 1f, lowFactor = 0.15f;
    private float lowpass;

    public enum TerrainType
    {
        HALLWAY, GRASS, SAND, ROOM
    };

    TerrainType terrain;

    void Awake()
    {
        lowpass = maxLow;
        terrain = TerrainType.ROOM;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (lowpass < maxLow)
        {
            lowpass += lowFactor*Time.deltaTime;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("explosionDistance", lowpass);
        }
    }

    public void setLow(float l)
    {
        lowpass = l;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("explosionDistance", l);
    }

    public void setTerrain(int t)
    {
        terrain = (TerrainType)t;
    }
    public int getTerrain()
    {
        return (int)terrain;
    }
    
}
