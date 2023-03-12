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

     enum state { stop, playing, ended };
     state estado = state.stop;

    public void setStatePlaying() { 
        estado = state.playing;
    }

    public void playGrab()
    {
        if (!recordEvent.IsPlaying() || recordEvent.IsPaused())
        {
            recordEvent.StartEvent();
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
        else
        {
            estado = state.stop;
        }
    }
}
