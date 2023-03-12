using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScreenSpawnExplosion : MonoBehaviour
{
    [SerializeField] Transform explodeTransform;
    [SerializeField] Material shatterMaterial;
    [SerializeField] FadeChangeScene fade;
    [SerializeField] MainMenuTransitionController transitionController;
    [SerializeField] GameObject startMenuCanvas, mainMenuCanvas;

    private bool initiated = false;

    public void Update()
    {
        if (Input.anyKeyDown && !initiated)
        {
            transitionController.StartGame();
            mainMenuCanvas.SetActive(true);
            startMenuCanvas.SetActive(false);
            initiated = true;
        }
    }

    public void ShatterScreen()
    {
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

        yield return new WaitForSeconds(1.5f);
        fade.OnFadeComplete();

    }
}
