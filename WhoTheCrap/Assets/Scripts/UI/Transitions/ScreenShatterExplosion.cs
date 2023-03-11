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
         foreach(Transform child in transform)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                childRigidbody.AddExplosionForce(explosionForce, explosionPoint.position, explosionRadius);
        }
    }
}
