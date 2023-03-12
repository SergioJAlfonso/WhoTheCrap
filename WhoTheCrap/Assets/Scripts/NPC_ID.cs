using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ID : MonoBehaviour
{
    [SerializeField]
    int id;

    bool objective = false;

    public Transform zoomPoint;

    [SerializeField] bool canTalk;


    void Start()
    {
        if (GameManager.instance.checkId(id))
        {
            GameManager.instance.registerObjectiveTransform(transform, zoomPoint);
            objective = true;
        }
        if (canTalk)
        {
            //Debug.Log("Can Talk");
            if (GetComponent<PlayEvent>() == null)
                Debug.Log("NPC can talk - No PlayEvent found");
            else
            {
                GameManager.instance.registerNPCSoundBehabiour(this.GetComponent<NPCSoundBehaviour>());
                GetComponent<NPCSoundBehaviour>().ChangeActualState(0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            transform.GetChild(3).gameObject.SetActive(false);
            GameManager.instance.EndGame(objective);
        }
    }

    public bool getObjective()
    {
        return objective;
    }
}
