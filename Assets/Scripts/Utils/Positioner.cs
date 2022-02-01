using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Positioner
{

    public static Vector3[] PositionAround(Transform center, int count, int unit = 1)
    {
        var result = new Vector3[count];
        if (count > 0)
        {
            var delta = 360f / count;
            var transform = MonoBehaviour.Instantiate(center);
            transform.SetParent(center);
            transform.localPosition = Vector3.back * unit;
            result[0] = transform.localPosition;
            for (int i = 1; i < count; i++)
            {
                transform.RotateAround(center.position, Vector3.up, delta);
                result[i] = transform.localPosition;
            }
            MonoBehaviour.Destroy(transform.gameObject);
        }
        return result;
    }
}
