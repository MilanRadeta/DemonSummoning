using UnityEngine;

public static class Layers
{

    public static string UI = "UI";

    public static void ChangeLayers(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayers(child.gameObject, layer);
        }
    }

}
