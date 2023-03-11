using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateIndvApp : MonoBehaviour
{
    [SerializeField]
    public TextMeshPro texto;
    [SerializeField]
    public int totalSuspects;

    public void GenerateScuspect()
    {
        //de momento asi, luego hago una lista que suffle al principio de la partida.
        int res = Random.Range(0, totalSuspects);
        texto.text = "Sospechoso \n ID: " + res;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
