using System.Collections.Generic;
using UnityEngine;

public abstract class GetCards : MonoBehaviour
{
    public abstract IEnumerable<Card> Cards { get; }
    protected GameController Game { get { return GameController.Instance; } }
    protected Card card;

    void Start()
    {
        card = GetComponent<Card>();
    }


}
