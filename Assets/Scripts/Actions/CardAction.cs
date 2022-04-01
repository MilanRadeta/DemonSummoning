using System.Collections;
using UnityEngine;

public abstract class CardAction : MonoBehaviour
{
    public Card Card { get { return card; } }
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
        if (Next != null)
        {
            yield return Next.Execute();
        }
    }
}
