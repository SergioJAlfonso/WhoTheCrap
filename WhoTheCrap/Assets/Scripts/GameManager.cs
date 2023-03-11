using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


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


    enum Gamestate { MENU, PLAYING, TIMELIMIT, WRONGANSWER, CORRECT };

    private bool isGameOver = false, win;
    Gamestate gState = Gamestate.PLAYING;

    int id = 345; //TODO id placeholder para pruebas, hacer que coincida con el de la escena anterior (meter ID)

    private const float gamePlaceholderTime = 60; //TODO tiempo placeholder, el tiempo final irá asociado a la duración de audio de cada id
    private float gameElapsedTime = 60; //TODO tiempo placeholder, el tiempo final irá asociado a la duración de audio de cada id

    private const float oriFirstTextTime = 2f;
    private float firstTextElapsedTime = 2f;

    private const float oriSecondTextTime = 0.75f;
    private float secondTextElapsedTime = 0.75f;
    [SerializeField]
    private float finalElapsedTime = 0.75f;

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

    LookAt[] lookAtTargets = new LookAt[0];

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

    public void registerLookAt(LookAt la)
    {
        Array.Resize(ref lookAtTargets, lookAtTargets.Length + 1);
        lookAtTargets[lookAtTargets.Length - 1] = la;
    }
    public void registerText(String st, InitialInstruction ii)
    {
        switch (st)
        {
            case "wrong":
                wrongText = ii;
                break;
            case "answer":
                answerText = ii;
                break;
            case "correct":
                correctText = ii;
                break;
            case "time":
                timeText = ii;
                break;
            case "limit":
                limitText = ii;
                break;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (lowpass < maxLow)
        {
            lowpass += lowFactor * Time.deltaTime;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("explosionDistance", lowpass);
        }

        if (gState == Gamestate.PLAYING)
        {
            //TODO: enablear solo una vez, refactorizar esto que estoy haciendo una escabechina
            gameElapsedTime -= Time.deltaTime;
            if (gameElapsedTime <= 0)
            {
                //Debug.Log("TimeLimit");
                secondTextElapsedTime -= Time.deltaTime;
                timeText.enabled = true;
                if (secondTextElapsedTime <= 0)
                {
                    finalElapsedTime -= Time.deltaTime;
                    limitText.enabled = true;

                    if (finalElapsedTime <= 0)
                        gState = Gamestate.TIMELIMIT;
                }
            }
            else if (isGameOver)
            {
                //TODO: 

                firstTextElapsedTime -= Time.deltaTime;

                if (win && firstTextElapsedTime <= 0)
                {
                    //Debug.Log("Correct");
                    correctText.enabled = true;
                    finalElapsedTime -= Time.deltaTime;

                    if (finalElapsedTime <= 0)
                        gState = Gamestate.CORRECT;
                }
                else if (firstTextElapsedTime <= 0)
                {
                    //Debug.Log("Wrong Answer");
                    secondTextElapsedTime -= Time.deltaTime;
                    wrongText.enabled = true;
                    if (secondTextElapsedTime <= 0)
                    {
                        answerText.enabled = true;
                        finalElapsedTime -= Time.deltaTime;

                        if (finalElapsedTime <= 0)
                            gState = Gamestate.WRONGANSWER;
                    }
                }
                else
                {
                    //Look at player
                    foreach (LookAt la in lookAtTargets)
                    {
                        la.enabled = true;
                    }
                }
            }
        }
        
        else if(gState == Gamestate.TIMELIMIT || gState == Gamestate.WRONGANSWER || gState == Gamestate.CORRECT)
        {
            gState = Gamestate.PLAYING;
            isGameOver = false;
            gameElapsedTime = gamePlaceholderTime;
            firstTextElapsedTime = oriFirstTextTime;
            finalElapsedTime = secondTextElapsedTime = oriSecondTextTime;
            lookAtTargets = new LookAt[0];

            SceneManager.LoadScene("Sergio");
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
