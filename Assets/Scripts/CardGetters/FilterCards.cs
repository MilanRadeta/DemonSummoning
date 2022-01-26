using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FilterCards : GetCards
{

    public GetCards cardsGetter;
    public CardType[] allowedTypes;

    void Start()
    {
        if (cardsGetter == null)
        {
            Debug.LogError("FilterCards.cardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerable<Card> Cards { get { return cardsGetter.Cards.Where(c => allowedTypes.Contains(c.type)); } }
}
