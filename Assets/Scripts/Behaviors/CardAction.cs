using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CardAction : MonoBehaviour
{
    public CardAction Next { get; set; }
    protected GameController Game { get { return GameController.Instance; } }
    protected Card card;

    void Start()
    {
        card = GetComponent<Card>();
    }

    public abstract bool Execute();
    public virtual IEnumerator ExecuteChain()
    {
        if (Execute())
        {
            yield return Next;
        }
    }
}
