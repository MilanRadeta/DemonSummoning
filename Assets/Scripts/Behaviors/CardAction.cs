using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CardAction : MonoBehaviour
{
    public CardAction Next { get; set; }
    protected Card card;
    protected GameController game;

    void Start()
    {
        card = GetComponent<Card>();
        game = GameController.Instance;
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
