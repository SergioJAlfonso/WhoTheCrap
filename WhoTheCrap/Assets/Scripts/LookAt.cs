using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    private Vector3 startPosition;
    private float desiredDuration = 0.5f;
    private float elapsedTime;

    public NPC_ID npcID;
    public Animator characterAnim;

    [SerializeField]
    private AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        GameManager.instance.registerLookAt(this);
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector3.Lerp(startPosition, target.position, curve.Evaluate(percentageComplete));
    }

    private void OnEnable()
    {
        if(npcID.getObjective())
            this.enabled = false;
        else
        {
            characterAnim.StopPlayback();
        }
    }
}
