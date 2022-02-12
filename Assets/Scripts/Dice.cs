using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dice : MonoBehaviour
{

    public int[] Numbers { get { return dice.Select(d => d.Number).ToArray(); } }
    public int Sum { get { return Numbers.Sum(); } }
    public float showDuration = 1f;
    private IEnumerable<Die> dice;

    void Awake()
    {
        dice = transform.GetComponentsInChildren<Die>();
    }

    public void Roll()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        foreach (var die in dice)
        {
            StartCoroutine(die.Roll());
        }
        StartCoroutine(Hide());
    }

    public bool HasNumbers(bool checkActive = true)
    {
        return (!checkActive || !gameObject.activeSelf) && Numbers.All(n => n > 0);
    }

    private IEnumerator Hide()
    {
        yield return new WaitUntil(() => this.HasNumbers(false));
        yield return new WaitForSeconds(showDuration);
        gameObject.SetActive(false);
    }

}
