using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ID : MonoBehaviour
{
    [SerializeField]
    int id;

    bool objective = false;

    public Transform zoomPoint;

    void Start()
    {
        if (GameManager.instance.checkId(id))
        {
            GameManager.instance.registerObjectiveTransform(transform,zoomPoint);
            objective = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            GameManager.instance.EndGame(objective);
    }

    public bool getObjective()
    {
        return objective;
    }
}
