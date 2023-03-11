using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    EventReference stepRef;

    Animator animator;

    private static readonly int HashMovement = Animator.StringToHash("Movement");


    const float maxStep = .15f;
    float stepTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (stepTimer > 0.0f) stepTimer -= Time.deltaTime;
    }

    public void step()
    {
        if (stepTimer > 0.0f) return;
        
        if (animator.GetFloat(HashMovement) < 0.01f && animator.GetFloat(HashMovement) > 0.0f)
            return;

        EventInstance stepEv = RuntimeManager.CreateInstance(stepRef);

        stepEv.start();
        stepEv.release();

        stepTimer = maxStep;
    }
}
