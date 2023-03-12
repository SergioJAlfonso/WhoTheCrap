using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTransitionController : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            GoBackToStart();
        if(Input.GetKeyDown(KeyCode.Alpha2))
            GoCodeScreen();
        if(Input.GetKeyDown(KeyCode.Alpha3))
            GoSettingsScreen();
    }

    public void StartGame()
    {
        animator.SetFloat("Init", 1f);
    }

    public void GoCodeScreen()
    {
        animator.SetTrigger("GoCode");
    }

    public void GoSettingsScreen()
    {
        animator.SetTrigger("GoSettings");
    }

    public void GoBackToStart()
    {
        animator.SetTrigger("GoMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
