using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RegisPPVolume : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.registerVignette(GetComponent<Volume>());
    }
}
