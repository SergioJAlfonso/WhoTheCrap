using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour
{
    public float timeBetweenAnim;
    public TextMeshProUGUI textbox;

    private float timer;
    private int numPoints = 1;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAnim)
        {
            timer = 0;
            numPoints = numPoints + 1 > 3 ? 1 : numPoints + 1;
            textbox.text = "LOADING";
            for(int i = 0; i < numPoints; i++){
                textbox.text = textbox.text + ".";
            }
        }
    }
}
