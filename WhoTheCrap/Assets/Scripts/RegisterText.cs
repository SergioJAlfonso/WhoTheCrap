using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterText : MonoBehaviour
{
    [SerializeField]
    string textToRegister;
    void Start()
    {
        GameManager.instance.registerText(textToRegister, gameObject.GetComponent<InitialInstruction>());
    }
}
