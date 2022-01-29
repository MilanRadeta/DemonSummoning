using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTransform : MonoBehaviour
{
    public bool FaceUp
    {
        set
        {
            if (Application.isEditor)
            {
                transform.rotation = Quaternion.Euler(0, 0, value ? 0 : 180);
            }

        }
    }
    public Vector3 TargetPosition
    {
        get { return targetPosition; }
        set
        {
            targetPosition = value;
            distance = Vector3.Distance(value, transform.localPosition);
            if (Application.isEditor)
            {
                transform.localPosition = targetPosition;
            }
        }
    }
    public float Speed { get; set; } = 1f;
    private Vector3 targetPosition = Vector3.zero;
    private float distance = 0f;

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        var epsilon = 0.001f;
        if (Speed <= 0f || Vector3.Distance(transform.localPosition, TargetPosition) < epsilon)
        {
            transform.localPosition = TargetPosition;
            return;
        }
        float step = distance * Speed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPosition, step);
    }

}
