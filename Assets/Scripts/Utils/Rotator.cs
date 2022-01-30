using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Translator
{
    public override float Distance { get { return Vector3.Angle(Current, Target); } }
    public override Vector3 Current { get { return transform.localRotation.eulerAngles; } set { transform.localRotation = Quaternion.Euler(value); } }
    public override Vector3 Next { get { return Vector3.RotateTowards(Current, Target, Step, Step); } }

}
