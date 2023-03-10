using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FMODUnity;

public class PlayEvent : MonoBehaviour
{
    [Header("FMOD Settings")]
    [SerializeField] public EventReference EnginePath;

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
}

