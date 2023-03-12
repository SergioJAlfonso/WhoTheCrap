using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShatterExplosion : MonoBehaviour
{

    public float explosionForce = 10.0f;
    public float explosionRadius = 10.0f;
    public Transform explosionPoint;

    private void Awake()
    {
         StartCoroutine(WaitFewSecs());
    }

    IEnumerator WaitFewSecs()
    {
        yield return new WaitForSeconds(0.05f);
        Shatter();
    }

    void Shatter()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.useGravity = true;
                childRigidbody.AddExplosionForce(explosionForce, explosionPoint.position, explosionRadius);
            }
        }
    }
}
