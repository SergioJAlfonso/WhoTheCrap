using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    float animTimer = 2.0f, animAngle = 0.0f;

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.Euler(new Vector3(0, animAngle, 0)), animTimer*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
            return;

        animAngle = Random.Range(1000, 10000);
    }
}
