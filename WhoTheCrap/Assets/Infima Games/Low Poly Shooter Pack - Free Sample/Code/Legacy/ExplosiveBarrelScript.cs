﻿using UnityEngine;
using System.Collections;
using FMODUnity;
using FMOD.Studio;

public class ExplosiveBarrelScript : MonoBehaviour {

	float randomTime;
	bool routineStarted = false;

	//Used to check if the barrel 
	//has been hit and should explode 
	public bool explode = false;

	[Header("Prefabs")]
	//The explosion prefab
	public Transform explosionPrefab;
	//The destroyed barrel prefab
	public Transform destroyedBarrelPrefab;

	[Header("Customizable Options")]
	//Minimum time before the barrel explodes
	public float minTime = 0.05f;
	//Maximum time before the barrel explodes
	public float maxTime = 0.25f;

	[Header("Explosion Options")]
    [SerializeField]
    //How far the explosion will reach
    private float explosionRadius = 10f;
    //How powerful the explosion is
    [SerializeField]
    private float explosionForce = 4000.0f;

	public GameObject player;

    [SerializeField]
    private float lowpassRadius = 10f;

	[SerializeField]
	EventReference explosionEvRef;// = "event:/Explosions/Explosion";

    EventInstance explosionEv;

	[SerializeField]
	EventReference beepEvRef;// = "event:/Explosions/Beep";

    EventInstance beepEv;
	private void Awake()
	{
        explosionEv = RuntimeManager.CreateInstance(explosionEvRef);
        RuntimeManager.AttachInstanceToGameObject(explosionEv, transform);

        beepEv = RuntimeManager.CreateInstance(beepEvRef);
        RuntimeManager.AttachInstanceToGameObject(beepEv, transform);
    }
    private void Update () {
		//Generate random time based on min and max time values
		randomTime = Random.Range (minTime, maxTime);

		//If the barrel is hit
		if (explode == true) 
		{
			if (routineStarted == false) 
			{
				//Start the explode coroutine
				StartCoroutine(Explode());
				routineStarted = true;
			} 
		}
	}
	
	private IEnumerator Explode () {
		//Wait for set amount of time
		yield return new WaitForSeconds(randomTime);

		//Spawn the destroyed barrel prefab
		Instantiate (destroyedBarrelPrefab, transform.position, 
		             transform.rotation);

        //Audio
        float dist = Mathf.Abs((transform.position - player.transform.position).magnitude)/lowpassRadius;
        GameManager.instance.setLow(dist);

        explosionEv.start();
        explosionEv.release();

        beepEv.start();
        beepEv.release();

        //Explosion force
        Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
		foreach (Collider hit in colliders) {
			//Si es el jugador, no se aplica fuerza
			if (hit.gameObject == player)
                continue;

			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			
			//Add force to nearby rigidbodies
			if (rb != null)
				rb.AddExplosionForce (explosionForce * 50, explosionPos, explosionRadius);

			//If the barrel explosion hits other barrels with the tag "ExplosiveBarrel"
			if (hit.transform.tag == "ExplosiveBarrel") 
			{
				//Toggle the explode bool on the explosive barrel object
				hit.transform.gameObject.GetComponent<ExplosiveBarrelScript>().explode = true;
			}
				
			//If the explosion hit the tag "Target"
			if (hit.transform.tag == "Target") 
			{
				//Toggle the isHit bool on the target object
				hit.transform.gameObject.GetComponent<TargetScript>().isHit = true;
			}

			//If the explosion hit the tag "GasTank"
			if (hit.GetComponent<Collider>().tag == "GasTank") 
			{
				//If gas tank is within radius, explode it
				hit.gameObject.GetComponent<GasTankScript> ().isHit = true;
				hit.gameObject.GetComponent<GasTankScript> ().explosionTimer = 0.05f;
			}
		}

		//Raycast downwards to check the ground tag
		RaycastHit checkGround;
		if (Physics.Raycast(transform.position, Vector3.down, out checkGround, 50))
		{
			//Instantiate explosion prefab at hit position
			Instantiate (explosionPrefab, checkGround.point, 
				Quaternion.FromToRotation (Vector3.forward, checkGround.normal)); 
		}

		//Destroy the current barrel object
		Destroy (gameObject);
	}
}