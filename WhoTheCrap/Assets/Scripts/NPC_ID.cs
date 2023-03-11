using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ID : MonoBehaviour
{
    [SerializeField]
    int id;

    bool objective = false;

    void Start()
    {
        if (GameManager.instance.checkId(id))
            objective = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("col");
        if (collision.gameObject.CompareTag("Bullet"))
            GameManager.instance.EndGame(objective);
    }
}
