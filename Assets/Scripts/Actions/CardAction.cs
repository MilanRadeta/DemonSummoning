using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CardAction : MonoBehaviour
{
    public static CardAction CurrentAction { get; set; }
    public virtual bool IsConfirmable { get; } = false;
    public bool Confirmed { get; set; } = false;
    public CardAction Next;
    protected GameController Game { get { return GameController.Instance; } }
    protected Card card;

    void Start()
    {
        card = GetComponent<Card>();
    }

    public abstract IEnumerator Execute();
    protected virtual IEnumerator ExecuteNext()
    {
        CurrentAction = Next;
        if (Next != null)
        {
            yield return Next.ExecuteNext();
        }
    }
}
