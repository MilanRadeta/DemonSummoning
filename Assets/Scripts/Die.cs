using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Die : MonoBehaviour
{

    public readonly int MinValue = 1;
    public readonly int MaxValue = 6;

    public Animator animator;
    public Vector3[] rotationsForDiceValue = new Vector3[] {
        new Vector3(-90, 180, 0),
        new Vector3(0, -90, -90),
        new Vector3(0, 0, 0),
        new Vector3(0, 0, 180),
        new Vector3(0, 90, 90),
        new Vector3(90, 0, 0),
    };
    public int animationDurationInSeconds = 2;

    public int Number
    {
        get
        {
            return this.number;
        }
        set
        {
            number = value;
            animator.enabled = true;
            animator.SetInteger("Value", value);
            if (value >= MinValue && value <= MaxValue)
            {
                animator.enabled = false;
                Debug.Log(value);
                Debug.Log(rotationsForDiceValue[value - 1]);
                transform.rotation = Quaternion.Euler(rotationsForDiceValue[value - 1]);
            }
        }
    }
    private int number = 1;

    public IEnumerator Roll()
    {
        Number = 0;
        yield return new WaitForSeconds(animationDurationInSeconds);
        Number = Random.Range(MinValue, MaxValue);
    }

}
