using FMODUnity;
using UnityEngine;

public class PlayEvent : MonoBehaviour
{
    [Header("FMOD Settings")]
    [Header("EventPath")]
    [SerializeField] public EventReference EventPath;

    [SerializeField] bool is3D;

    [SerializeField] bool initialParameter;
    [SerializeField] string parameterName;
    [SerializeField] int initialValue;

    [SerializeField] bool PlayonStart;

    FMOD.Studio.EventInstance Event;
    public FMOD.Studio.EventInstance GetEvent() { return Event; }   

    void Start()
    {
        Event = FMODUnity.RuntimeManager.CreateInstance(EventPath);

        if (is3D) {
            // Establecemos la posición inicial del evento en el mundo 3D
            Vector3 posicion = transform.position;
            FMOD.ATTRIBUTES_3D atributos = RuntimeUtils.To3DAttributes(posicion);
            Event.set3DAttributes(atributos);
        }

        if (initialParameter)
        {
            SetParameterByName(parameterName, initialValue);
        }

        if (PlayonStart)
        {
            StartEvent();
        }
    }

    private void Update()
    {
        if (is3D)
        {
            // Actualizamos la posición del evento en el mundo 3D cada frame
            Vector3 posicion = transform.position;
            FMOD.ATTRIBUTES_3D atributos = RuntimeUtils.To3DAttributes(posicion);
            Event.set3DAttributes(atributos);
        }
    }

    private void OnDestroy()
    {
        // Liberamos la instancia del evento cuando el objeto es destruido
        Event.release();
    }

    public void StartEvent()
    {
        ResumeEvent();
        Event.start();
    }

    public void PauseEvent()
    {
        Event.setPaused(true);
    }

    public void ResumeEvent()
    {
        if (IsPaused())
            Event.setPaused(false);
    }

    public void StopEvent()
    {
        Event.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void FadeStopEvent()
    {
        Event.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    }

    public void SetParameterByName(string parameterName, int value)
    {
        Event.setParameterByName(parameterName, value);
    }

    //
    public bool IsPlaying()
    {
        Event.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    public bool IsPaused()
    {
        Event.getPaused(out bool paused);
        return paused;
    }

   
}

