using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TargetCustomScript : MonoBehaviour
{

	//Used to check if the target has been hit
	bool isHit = false;

	TargetController controller;

	[Header("Animations")]
	public AnimationClip targetUp;
	public AnimationClip targetDown;

	[SerializeField]
	EventReference targetOrder/* = "event:/Gameplay/TargetOrder"*/;
	[SerializeField]
	EventReference targetMovement/* = "event:/Gameplay/TargetHit"*/;

	EventInstance orderEv;
	EventInstance movementEv;

	public void Awake()
    {
		controller = GetComponentInParent<TargetController>();
		orderEv = RuntimeManager.CreateInstance(targetOrder);
		movementEv = RuntimeManager.CreateInstance(targetMovement);
		RuntimeManager.AttachInstanceToGameObject(movementEv, transform);
	}

	public void PopTargetDown()
    {
		if (isHit) return;

		//Animate the target "down"
		gameObject.GetComponent<Animation>().clip = targetDown;
		gameObject.GetComponent<Animation>().Play();

		//Down sound
		movementEv.setParameterByName("Hit", 0);
		movementEv.start();

		//Set the sound to the target and play it
		orderEv.setParameterByName("NumTargets", controller.TargetDown());
		orderEv.start();

		isHit = true;
	}

	public void PopTargetUp()
    {
		//Animate the target "up" 
		gameObject.GetComponent<Animation>().clip = targetUp;
		gameObject.GetComponent<Animation>().Play();

		//Set the upSound as current sound, and play it
		movementEv.setParameterByName("Hit", 1);
		movementEv.start();

		//Target is no longer hit
		isHit = false;
	}

}
