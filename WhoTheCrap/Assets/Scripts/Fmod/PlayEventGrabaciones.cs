using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;

public class PlayEventGrabaciones : MonoBehaviour
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
    public void playGrab(int index)
    {
        engineEvent = FMODUnity.RuntimeManager.CreateInstance(GrabacionesPath[index]);
        playEngine();
    }
}

