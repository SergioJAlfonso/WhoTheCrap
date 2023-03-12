using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterInputField : MonoBehaviour
{
    TMP_InputField iField;
    
    void Start()
    {
        iField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    public void regIField()
    {
        GameManager.instance.registerID(iField.text);
    }
}
