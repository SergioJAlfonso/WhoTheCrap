using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSoundBehaviour : MonoBehaviour
{
    [SerializeField] string[] statesList;
    [SerializeField] PlayEvent[] eventsList;

    int actualState = 0;

    private void Start()
    {
        if (!eventsList[actualState].IsPlaying() || eventsList[actualState].IsPaused())
        {
            eventsList[actualState].StartEvent();
        }
    }

    private void OnDestroy()
    {
        foreach (PlayEvent pe in eventsList)
        {
            pe.GetEvent().release();
        }
    }

    public void ChangeActualState(int newState)
    {
        if (statesList[newState] != statesList[actualState])
        {
            Debug.Log("Last state:" + statesList[actualState]);
            //Pause last event
            if (actualState < eventsList.Length)
                eventsList[actualState].PauseEvent();

            actualState = newState;                             //Change the State
            Debug.Log("New state:" + statesList[actualState]);

            //Play new Event
            if (actualState < eventsList.Length)
            {
                if (!eventsList[actualState].IsPlaying() || eventsList[actualState].IsPaused())
                {
                    Debug.Log("Machine State Event " + statesList[actualState]);
                    eventsList[actualState].StartEvent();
                }
            }
        }
    }
}
