using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Remover
{

    public static void Destroy(Object obj)
    {
        if (Application.isEditor)
        {
            MonoBehaviour.DestroyImmediate(obj);
        }
        else
        {
            MonoBehaviour.Destroy(obj);
        }
    }

}
