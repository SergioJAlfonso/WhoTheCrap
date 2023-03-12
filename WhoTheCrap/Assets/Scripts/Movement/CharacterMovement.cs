using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct WaitPoint
{
    public Transform point;
    public float waitTime;
}

public class CharacterMovement : MonoBehaviour
{
    public WaitPoint[] points;

    private Rigidbody rigidbody;

    public float speed;

    private int pointIndex = 0;

    private float wait = 0;

    private Color gizmoColor;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gizmoColor = Random.ColorHSV();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = points[pointIndex].point.position;
        position.y = transform.position.y;

        rigidbody.MovePosition(Vector3.MoveTowards(transform.position, position, speed * Time.fixedDeltaTime));

        if ((transform.position - position).magnitude < 0.1f){
            wait += Time.deltaTime;

            if(wait >= points[pointIndex].waitTime)
                pointIndex = pointIndex + 1 >= points.Length ? 0 : pointIndex + 1;
        }
    }

    private void OnDrawGizmos()
    {
        GUI.color = gizmoColor;
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawWireSphere(points[i].point.position, .5f);
            //Handles.Label(points[i].point.position, ""+i);
        }
    }
}
