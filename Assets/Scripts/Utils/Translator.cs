using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{

    public virtual bool IsMoving { get { return Target != Current; } }
    public Vector3 Target
    {
        get { return target; }
        set
        {
            target = value;
            distance = Distance;
        }
    }
    public float Speed { get; set; } = 1f;
    public virtual float Distance { get { return Vector3.Distance(Current, Target); } }
    public virtual Vector3 Current { get { return transform.localPosition; } set { transform.localPosition = value; } }
    public float Step { get { return distance * Speed * Time.deltaTime; } }
    public virtual Vector3 Next { get { return Vector3.MoveTowards(Current, Target, Step); } }
    private Vector3 target = Vector3.zero;
    private float distance = 0f;

    void Update()
    {
        var epsilon = 0.001f;
        if (Speed <= 0f || Distance < epsilon)
        {
            Current = Target;
            return;
        }
        Current = Next;
    }

}
