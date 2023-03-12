using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class emitterController : MonoBehaviour
{


    [SerializeField]
    public GenerateIndvApp genIndv;
    [SerializeField]
    public CamRotateApp camRotate;

    [SerializeField] public PlayEvent recordEvent;

     enum state { stop, ready, playing, ended };
     state estado = state.stop;

    public void setStatePlaying() { 
        estado = state.playing;
    }
    public void setStateReady()
    {
        estado = state.ready;
    }


    public void playGrab()
    {
        if (estado == state.ready)
        {
            if (!recordEvent.IsPlaying() || recordEvent.IsPaused())
            {
                recordEvent.StartEvent();
                estado = state.playing;
            }

        }
    }

    private void Update()
    {
        if (estado == state.playing)
        {
            if (!recordEvent.IsPlaying())
            {
                camRotate.EventEnd();
                //girar la camara
                estado = state.ended;
                genIndv.nextInd();

                estado = state.stop;
            }

        }
    }
}
