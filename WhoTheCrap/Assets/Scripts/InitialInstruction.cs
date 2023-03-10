using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitialInstruction : MonoBehaviour
{
    TMP_Text text;
    [SerializeField]
    string[] textInput;
    [SerializeField]
    float[] initialTime;

    float textElapsedTime;

    short currentText = 0;

    private Vector3 endScale = new Vector3(1, 1, 1);
    private Vector3 startScale;
    private Vector3 originalScale;

    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private bool enablePlayerInput = false;
    [SerializeField]
    private bool mute = false;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = startScale = transform.localScale;

        text = GetComponent<TMP_Text>();

        text.text = textInput[currentText];
        textElapsedTime = initialTime[currentText];

        if (!mute)
            GameManager.instance.GetFmodGameManager().transition_event.StartEvent();
    }

    // Update is called once per frame
    void Update()
    {
        textElapsedTime -= Time.deltaTime;

        if (currentText < textInput.Length)
        {
            float percentageComplete = (initialTime[currentText] - textElapsedTime) / initialTime[currentText];
            transform.localScale = Vector3.Lerp(startScale, endScale, curve.Evaluate(percentageComplete));
        }

        if (textElapsedTime <= 0)
        {
            if (currentText == textInput.Length - 1)
            {
                if (enablePlayerInput) GameManager.instance.enablePlayerInput();
                Destroy(this.gameObject);
            }
            else
            {
                currentText++;
                text.text = textInput[currentText];
                textElapsedTime = initialTime[currentText];
                transform.localScale = originalScale;

                if(!mute)
                    GameManager.instance.GetFmodGameManager().transition_event.StartEvent();
            }
        }
    }
}
