using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdList : MonoBehaviour
{
    [SerializeField]
    public int[] ids;

    public bool exist(int id)
    {
        for (int i =0; i < ids.Length; i++)
        {
            if (ids[i] == id)
                return true;
        }

        return false;
    }
    /* Para recoger la id para los audios y logica del jeugo que utilizan los audios
     * y el registro del disparo
     * -1 es error */
    public int getIndex(int id)
    {
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == id)
                return i;
        }

        return -1;
    }
    /* -1 es error */
    public int getId(int index)
    {
        if (index < ids.Length)
            return ids[index];
        return -1;
    }

}
