using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterCamera : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.registerCamera(transform);
    }

}
