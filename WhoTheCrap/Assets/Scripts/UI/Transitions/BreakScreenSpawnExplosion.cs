using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakScreenSpawnExplosion : MonoBehaviour
{
    [SerializeField] Transform explodeTransform;
    [SerializeField] Material shatterMaterial;
    [SerializeField] FadeChangeScene fade;
    [SerializeField] MainMenuTransitionController transitionController;
    [SerializeField] GameObject startMenuCanvas, mainMenuCanvas;
    [SerializeField] UnityEvent uiEventStart, uiEventShatter;

    private bool initiated = false;
    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Update()
    {
        if (Input.anyKeyDown && !initiated)
        {
            transitionController.StartGame();
            mainMenuCanvas.SetActive(true);
            startMenuCanvas.SetActive(false);
            initiated = true;
            uiEventStart.Invoke();
        }
    }

    public void ShatterScreen()
    {
        if (!GameManager.instance.checkId(-1))
            StartCoroutine(CoroutineScreenshot());
    }

    IEnumerator CoroutineScreenshot()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshotTexture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);  
        Rect rect = new Rect(0,0, width, height);
        screenshotTexture2D.ReadPixels(rect, 0, 0);
        screenshotTexture2D.Apply();

        shatterMaterial.SetTexture("_BaseMap", screenshotTexture2D);

        explodeTransform.gameObject.SetActive(true);
        uiEventShatter.Invoke();

        yield return new WaitForSeconds(1.6f);
        fade.OnFadeComplete();

    }
}
