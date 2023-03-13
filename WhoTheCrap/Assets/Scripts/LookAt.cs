using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public Transform parentTr;

    private Vector3 startPosition;
    private float desiredDuration = 0.5f;
    private float elapsedTime;

    public NPC_ID npcID;
    public Animator characterAnim;

    [SerializeField]
    private AnimationCurve curve;

    Vector3 direction;
    float angle;

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

        // Si el ángulo es mayor que 90 grados, rotar el objeto 2 hacia el objeto 1
        if (angle > 90f || angle < -90)
        {
            // Calcular la rotación deseada para el objeto 2
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y + 45f, 0f);

            // Interpolar la rotación actual del objeto 2 hacia la rotación deseada
            parentTr.rotation = Quaternion.Lerp(target.rotation, targetRotation, curve.Evaluate(percentageComplete));
        }
    }

    private void OnEnable()
    {
        if(npcID.getObjective())
            this.enabled = false;
        else
        {
            characterAnim.StopPlayback();
            //Debug.Log(target + " " + parentTr);
            direction = target.position - parentTr.position;
            angle = Vector3.Angle(direction, parentTr.up);
        }
    }
}
