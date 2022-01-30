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
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, value ? 0 : 180);
            }

        }
    }
    public Vector3 TargetPosition
    {
        get { return translator.Target; }
        set { translator.Target = value; }
    }
    public Vector3 TargetRotation
    {
        get { return rotator.Target; }
        set { rotator.Target = value; }
    }
    public Translator translator;
    public Rotator rotator;

    void Update()
    {
        translator.Translate();
        rotator.Translate();
    }

}
