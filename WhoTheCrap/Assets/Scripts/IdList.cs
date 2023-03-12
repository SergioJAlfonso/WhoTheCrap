using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdList : MonoBehaviour
{
    [SerializeField]
    public string[] ids;

    public bool exist(string id)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == id)
                return true;
        }

        return false;
    }
    /* Para recoger la id para los audios y logica del jeugo que utilizan los audios
     * y el registro del disparo
     * -1 es error */
    public int getIndex(string id)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == id)
                return i;
        }

        return -1;
    }
    /* -1 es error */
    public string getId(int index)
    {
        if (index < ids.Length)
            return ids[index];
        return null;
    }


    public int totalIds()
    {
        return ids.Length;
    }
}
