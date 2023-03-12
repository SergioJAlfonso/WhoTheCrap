using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using static GameManager;

public class FmodGameManager : MonoBehaviour
{
    public static FmodGameManager instance { get; private set; }

    //Music Events
    [SerializeField] public PlayEvent music_event;
    [SerializeField] public PlayEvent breeze_event;
    [SerializeField] public PlayEvent clock_event;
    [SerializeField] public PlayEvent zoom_event;
    [SerializeField] public PlayEvent transition_event;
    [SerializeField] public PlayEvent heart_event;

    void Awake()
    {

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
}
