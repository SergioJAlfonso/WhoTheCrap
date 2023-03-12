using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;

public class PlayEvent : MonoBehaviour
{

   

    [Header("FMOD Settings")]
    [SerializeField] public EventReference EnginePath;

    [Header("FMOD Settings")]
    [SerializeField] public EventReference[] GrabacionesPath;

    FMOD.Studio.EventInstance engineEvent;

    void Start()
    {
        engineEvent = FMODUnity.RuntimeManager.CreateInstance(EnginePath);
    }

    public void playEngine()
    {
        engineEvent.start();
        engineEvent.release();
    }
    public EventReference GetGrab(int index)
    {
        return GrabacionesPath[index];
        //engineEvent = FMODUnity.RuntimeManager.CreateInstance(GrabacionesPath[index]);
        //playEngine();
    }
}

