using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	[Header("Customizable Options")]
	//How long before the explosion prefab is destroyed
	public float despawnTime = 10.0f;
	//How long the light flash is visible
	public float lightDuration = 0.02f;
	[Header("Light")]
	public Light lightFlash;

	private void Start () {
		//Start the coroutines
		StartCoroutine (DestroyTimer ());
		StartCoroutine (LightFlash ());
	}

	private IEnumerator LightFlash () {
		//Show the light
		lightFlash.GetComponent<Light>().enabled = true;
		//Wait for set amount of time
		yield return new WaitForSeconds (lightDuration);
		//Hide the light
		lightFlash.GetComponent<Light>().enabled = false;
	}

	private IEnumerator DestroyTimer () {
		//Destroy the explosion prefab after set amount of seconds
		yield return new WaitForSeconds (despawnTime);
		Destroy (gameObject);
	}
}