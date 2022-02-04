using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dice : MonoBehaviour
{
    
    private IEnumerable<Die> dice;

    void Start()
    {
        dice = transform.GetComponentsInChildren<Die>();
    }

    public void Roll()
    {
        foreach (var die in dice)
        {
            StartCoroutine(die.Roll());
        }
    }

}
