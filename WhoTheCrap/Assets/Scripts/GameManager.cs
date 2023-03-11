using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

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


    enum Gamestate { MENU, PLAYING };

    private bool isGameOver = false, win;
    Gamestate gState = Gamestate.PLAYING;

    int id = 345; //TODO id placeholder para pruebas, hacer que coincida con el de la escena anterior (meter ID)

    private float gameElapsedTime = 60; //TODO tiempo placeholder, el tiempo final irá asociado a la duración de audio de cada id
    private float TextElapsedTime = 0.75f;

    [SerializeField]
    InitialInstruction wrongText;
    
    [SerializeField]
    InitialInstruction answerText;
    
    [SerializeField]
    InitialInstruction correctText;
    
    [SerializeField]
    InitialInstruction timeText;

    [SerializeField]
    InitialInstruction limitText;

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

    // Public method to end the game
    public void EndGame(bool victory)
    {
        isGameOver = true;
        win = victory;
    }

    public bool checkId(int otherId)
    {
        return (otherId == id);
    }

    // Update is called once per frame
    void Update()
    {
        if (lowpass < maxLow)
        {
            lowpass += lowFactor*Time.deltaTime;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("explosionDistance", lowpass);
        }

        if (gState == Gamestate.PLAYING)
        {
            gameElapsedTime -= Time.deltaTime;
            if (gameElapsedTime <= 0)
            {
                //Debug.Log("TimeLimit");
                TextElapsedTime -= Time.deltaTime;
                timeText.enabled = true;
                if (TextElapsedTime <= 0)
                {
                    limitText.enabled = true;
                    isGameOver = false; //TODO: de momento esto, mas adelante cambio de escena/paso al zoom
                }
            }
            else if (isGameOver)
            {
                if (win)
                {
                    //Debug.Log("Correct");
                    correctText.enabled = true; //TODO: de momento esto, mas adelante cambio de escena/paso al zoom
                    isGameOver = false; 
                }
                else
                {
                    //Debug.Log("Wrong Answer");
                    TextElapsedTime -= Time.deltaTime;
                    wrongText.enabled = true;
                    if (TextElapsedTime <= 0)
                    {
                        answerText.enabled = true;
                        isGameOver = false; //TODO: de momento esto, mas adelante cambio de escena/paso al zoom
                    }
                }
            }
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
