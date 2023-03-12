using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeChangeScene : MonoBehaviour
{
    public static FadeChangeScene instance;
    [SerializeField] Animator animator;
    private int levelToLoad;
    public bool fadeInCompleted { get; private set; }

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //instance = this;

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

    public void Start()
    {
        fadeInCompleted = false;
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        GameManager.instance.startPlaying(levelToLoad != 0 ? true : false);
        SceneManager.LoadScene(levelToLoad);
    }

    public void FadeToNextLevel()
    {
        if(!GameManager.instance.checkId(-1))
            FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeInCompleted()
    {
        fadeInCompleted = true;
    }
}
