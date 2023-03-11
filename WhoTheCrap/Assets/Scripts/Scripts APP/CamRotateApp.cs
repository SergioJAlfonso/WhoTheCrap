using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotateApp : MonoBehaviour
{
    Vector3 initRot;
    public Vector3 destRot;
    private Vector3 to;

    // Start is called before the first frame update
    void Start()
    {
        initRot = this.transform.rotation.eulerAngles;
    }

    private bool rotating = false;
    public void Update()
    {
        if (rotating)
        {
            if (Vector3.Distance(transform.eulerAngles, destRot) > 0.1f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
            }
            else
            {
                transform.eulerAngles = to;
                rotating = false;
            }
        }

    }


    public void EventPlay()
    {
        to = destRot;
        rotating = true;

        //this.transform.Rotate(0,120,0);
    }

    public void EventEnd()
    {
        to = initRot;
        rotating = true;
        //this.transform.Rotate(0,-120,0);
    }
}
