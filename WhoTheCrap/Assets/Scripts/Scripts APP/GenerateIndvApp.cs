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

    int[] indices;
    int index = 0;

    public void GenerateScuspect()
    {
        //de momento asi, luego hago una lista que suffle al principio de la partida.
        //int res = Random.Range(0, totalSuspects);
        texto.text = "Sospechoso \n ID: " + indices[index];
        index++;
        if (index >= totalSuspects)
        {
            index = 0;
            Shuffle(indices);
        }
    }


    void Shuffle(int[] a)
    {
        // Loops through array
        for (int i = a.Length - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            int temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }

        // Print
        //for (int i = 0; i < a.Length; i++)
        //{
        //    Debug.Log(a[i]);
        //}
    }

    void Start()
    {
        indices = new int[totalSuspects];
        for (int i = 0; i < totalSuspects; i++)
        {
            indices[i] = i;
        }

        Shuffle(indices);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}