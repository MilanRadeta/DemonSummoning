using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Layers
{

    public static void ChangeLayers(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }

}