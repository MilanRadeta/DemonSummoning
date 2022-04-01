using UnityEngine;

[RequireComponent(typeof(Translator), typeof(Rotator))]
public class CardTransform : MonoBehaviour
{
    public Translator translator;
    public Rotator rotator;
    public bool IsMoving { get { return translator.IsMoving || rotator.IsMoving; } }
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

}
