using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterInputField : MonoBehaviour
{
    TMP_InputField iField;
    IdList idList;
    
    void Start()
    {
        iField = GetComponent<TMP_InputField>();
        idList = GetComponent<IdList>();
    }

    // Update is called once per frame
    public void regIField()
    {
        if (idList.exist(iField.text))
        {
            GameManager.instance.registerID(idList.getIndex(iField.text));
        }
    }
}
