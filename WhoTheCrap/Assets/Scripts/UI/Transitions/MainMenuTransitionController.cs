using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTransitionController : MonoBehaviour
{
    [SerializeField] Animator animator;


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
