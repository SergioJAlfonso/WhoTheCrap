using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFromObject : MonoBehaviour
{
    [SerializeField] GameObject target;
    float distance;
    public float getDistance() { return distance; }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(distance);
        distance = Vector3.Distance(this.transform.position, target.transform.position);
        SetGlobalParameterByName("DistanceReverb", distance);
    }

    public void SetGlobalParameterByName(string name, float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(name, value);

    }
}
