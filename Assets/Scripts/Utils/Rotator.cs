using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Translator
{
    public override bool IsMoving { get { return Quaternion.Euler(Target) != transform.localRotation; } }
    public override float Distance { get { return Quaternion.Angle(transform.localRotation, Quaternion.Euler(Target)); } }
    public override Vector3 Current { get { return transform.localRotation.eulerAngles; } set { transform.localRotation = Quaternion.Euler(value); } }
    public override Vector3 Next { get { return Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(Target), Step).eulerAngles; } }

}
