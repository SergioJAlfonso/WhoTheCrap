using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;


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


    enum Gamestate { MENU, PLAYING, TIMELIMIT, WRONGANSWER, CORRECT, ZOOM, ENDING };

    private bool isGameOver = false, win;
    Gamestate gState = Gamestate.PLAYING;

    int id = 1; //TODO id placeholder para pruebas, hacer que coincida con el de la escena anterior (meter ID)

    //Timers
    private const float gamePlaceholderTime = 90; //TODO tiempo placeholder, el tiempo final irá asociado a la duración de audio de cada id
    private float gameElapsedTime = gamePlaceholderTime; //TODO tiempo placeholder, el tiempo final irá asociado a la duración de audio de cada id

    private const float oriFirstTextTime = 2f;
    private float firstTextElapsedTime = oriFirstTextTime;

    private const float oriSecondTextTime = 0.75f;
    private float secondTextElapsedTime = oriSecondTextTime;

    private float finalElapsedTime = oriSecondTextTime;

    //Texts
    InitialInstruction wrongText, answerText, correctText, timeText, limitText;

    LookAt[] lookAtTargets = new LookAt[0];

    PlayerInput playerInput;

    Transform objectiveTransform;
    Transform zoomTransform;
    Transform cameraTransform;


    //Zoom
    private Vector3 zoomStartPosition, zoomEndPosition;
    private float desiredZoomDuration = 0.5f;
    private const float zoomTime = 1f;
    private float zoomElapsedTime = 0;

    Quaternion startRotation, endRotation;

    [SerializeField]
    private AnimationCurve zoomLerpCurve;

    [SerializeField]
    int zoomRotationOffset = 1;

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

    public void registerID(string ID)
    {
        id = int.Parse(ID);
    }
    public void registerLookAt(LookAt la)
    {
        Array.Resize(ref lookAtTargets, lookAtTargets.Length + 1);
        lookAtTargets[lookAtTargets.Length - 1] = la;
    }
    public void registerPlayerInput(PlayerInput pi)
    {
        playerInput = pi;
    }
    public void registerText(string st, InitialInstruction ii)
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

    public void registerObjectiveTransform(Transform tr, Transform zoomTr)
    {
        objectiveTransform = tr;
        zoomTransform = zoomTr;
    }

    public void registerCamera(Transform tr)
    {
        cameraTransform = tr;
    }
    private void timeLimit()
    {
        if (playerInput.enabled == true)
        {
            playerInput.enabled = false;
            timeText.enabled = true;
        }

        secondTextElapsedTime -= Time.deltaTime;
        if (secondTextElapsedTime <= 0)
        {
            finalElapsedTime -= Time.deltaTime;

            if(limitText.enabled == false)
                limitText.enabled = true;

            if (finalElapsedTime <= 0)
                gState = Gamestate.TIMELIMIT;
        }
    }
    private void wrongAnswer()
    {
        secondTextElapsedTime -= Time.deltaTime;
        if (wrongText.enabled == false)
            wrongText.enabled = true;

        if (secondTextElapsedTime <= 0)
        {
            if (answerText.enabled == false)
                answerText.enabled = true;
            finalElapsedTime -= Time.deltaTime;

            if (finalElapsedTime <= 0)
                gState = Gamestate.WRONGANSWER;
        }
    }
    private void correct()
    {
        //Debug.Log("Correct");
        if (correctText.enabled == false)
            correctText.enabled = true;
        finalElapsedTime -= Time.deltaTime;

        if (finalElapsedTime <= 0)
            gState = Gamestate.CORRECT;
    }

    public void enablePlayerInput()
    {
        playerInput.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lowpass < maxLow)
        {
            lowpass += lowFactor * Time.deltaTime;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("explosionDistance", lowpass);
        }

        switch (gState)
        {
            case Gamestate.PLAYING:
                gameElapsedTime -= Time.deltaTime;

                if (gameElapsedTime <= 0)
                {
                    timeLimit();
                }
                else if (isGameOver)
                {
                    if (playerInput.enabled == true)
                        playerInput.enabled = false;

                    firstTextElapsedTime -= Time.deltaTime;

                    if (win && firstTextElapsedTime <= 0)
                    {
                        correct();
                    }
                    else if (firstTextElapsedTime <= 0)
                    {
                        wrongAnswer();
                    }
                    else
                    {
                        //Look at playersa
                        foreach (LookAt la in lookAtTargets)
                        {
                            la.enabled = true;
                        }
                    }
                }
                break;

            case Gamestate.TIMELIMIT:
            case Gamestate.WRONGANSWER:

                //Valores iniciales de lerp de camara
                //zoomStartPosition = Camera.main.transform.position;
                zoomStartPosition = cameraTransform.position;
                zoomEndPosition = zoomTransform.position;


                //startRotation = Camera.main.transform.rotation;
                startRotation = cameraTransform.rotation;
                Vector3 zoomEndRotationPosition = objectiveTransform.position;
                zoomEndRotationPosition.y += zoomRotationOffset;

                endRotation = Quaternion.LookRotation(zoomEndRotationPosition - zoomEndPosition, Vector3.up);
                gState = Gamestate.ZOOM;
                break;

            case Gamestate.ZOOM:

                //Lerp de camara
                zoomElapsedTime += Time.deltaTime;

                float percentageComplete = zoomElapsedTime / desiredZoomDuration;

                cameraTransform.rotation = Quaternion.Slerp(startRotation, endRotation, zoomLerpCurve.Evaluate(percentageComplete));
                cameraTransform.position = Vector3.Lerp(zoomStartPosition, zoomEndPosition, zoomLerpCurve.Evaluate(percentageComplete));
                //Camera.main.transform.rotation = Quaternion.Slerp(startRotation, endRotation, zoomLerpCurve.Evaluate(percentageComplete));
                //Camera.main.transform.position = Vector3.Lerp(zoomStartPosition, zoomEndPosition, zoomLerpCurve.Evaluate(percentageComplete));

                if (zoomElapsedTime >= zoomTime)
                    gState = Gamestate.ENDING;
                break;
            
            case Gamestate.CORRECT:

                zoomElapsedTime += Time.deltaTime;
                if (zoomElapsedTime >= zoomTime)
                    gState = Gamestate.ENDING;
                break;

            case Gamestate.ENDING:

                gState = Gamestate.PLAYING;
                isGameOver = false;
                gameElapsedTime = gamePlaceholderTime;
                firstTextElapsedTime = oriFirstTextTime;
                finalElapsedTime = secondTextElapsedTime = oriSecondTextTime;
                zoomElapsedTime = 0;
                lookAtTargets = new LookAt[0];

                SceneManager.LoadScene("Sergio");
                break;
        };
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
