using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertCodeShortcuts : MonoBehaviour
{
    public Button startButton;
    public Button backButton;

    bool submit = false;
    private void Update()
    {
        if (Input.GetAxis("Submit") > 0.01f && !submit){
            startButton.onClick.Invoke();
            submit = true;
        }

        if (Input.GetAxis("Submit") < 0.01f)
            submit = false;

        if (Input.GetKeyDown(KeyCode.Escape))
            backButton.onClick.Invoke();
    }
}
