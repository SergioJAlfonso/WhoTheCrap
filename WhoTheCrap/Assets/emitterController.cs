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
    [SerializeField]
    public StudioEventEmitter emitter;
    [SerializeField]
    public PlayEvent lista;

    enum state { stop, playing, ended};
    state estado = state.stop;

    public void playGrab(int index)
    {
        emitter.EventReference = lista.GetGrab(index);
        emitter.Play();
    }

    private void Update()
    {
        if (estado == state.playing)
        {
            if (!emitter.IsPlaying())
            {
                camRotate.EventEnd();
                //girar la camara
                estado = state.ended;
                genIndv.nextInd();
            }
        }
        else
        {
            estado = state.stop;
        }
    }
}
