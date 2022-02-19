using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FilterCards : GetCards
{

    public GetCards CardsGetter;
    public CardFilter cardFilter;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("FilterCards.CardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerable<Card> Cards
    {
        get
        {
            return CardsGetter.Cards.Where(c => cardFilter.IsAllowed(c));
        }
    }
}
