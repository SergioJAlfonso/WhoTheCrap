using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    TargetCustomScript[] targets;

    int numHit = 0;

    float waitTime = .5f;

    bool routineStarted = false;

    void Start()
    {
        targets = GetComponentsInChildren<TargetCustomScript>();
    }

    public float TargetDown()
    {
        //Indice de targets tirados
        numHit++;
        
        //Si golpeamos todos, hay que levantarlos de nuevo
        if (!routineStarted && numHit >= targets.Length)
        {
            StartCoroutine(PopTargets());
            
            routineStarted = true;
        }

        //Devuelve el valor del pitch que corresponde
        return (1.0f / (targets.Length-1)) * (numHit-1);
    }

    private IEnumerator PopTargets()
    {
        yield return new WaitForSeconds(waitTime);

        //Levantarlos a todos
        for (int i = 0; i < targets.Length; i++)
            targets[i].PopTargetUp();

        numHit = 0;

        routineStarted = false;
    }

}
