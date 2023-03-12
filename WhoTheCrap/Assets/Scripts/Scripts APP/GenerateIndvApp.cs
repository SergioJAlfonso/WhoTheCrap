using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateIndvApp : MonoBehaviour
{

    [SerializeField]
    public emitterController controler;
    [SerializeField]
    public TextMeshPro texto;
    [SerializeField]
    public int totalSuspects;
    [SerializeField]
    public IdList list;

    int[] indices;
    int index = 0;


    public void nextInd()
    {
        index++;
        if (index >= totalSuspects)
        {
            index = 0;
            Shuffle(indices);
        }
    }

    public void playGrab()
    {
        Debug.Log("Record Id " + indices[index]);
        controler.recordEvent.SetParameterByName("Record Id", indices[index]);
        //Playear grabacion id "index"
        controler.playGrab();
        //controler.setStatePlaying();
    }

    public void GenerateScuspect()
    {
        texto.text = "# Expediente \n: " + list.getId(indices[index]);//indices[index];
        controler.setStateReady();
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
