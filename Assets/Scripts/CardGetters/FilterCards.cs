using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FilterCards : GetCards
{

    public GetCards cardsGetter;
    public CardType[] allowedTypes;
    public CardAffinity[] allowedAffinities;

    void OnValidate()
    {
        if (cardsGetter == null)
        {
            throw new MissingReferenceException("FilterCards.cardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerable<Card> Cards
    {
        get
        {
            return cardsGetter.Cards.Where(c =>
                IsAllowed(allowedTypes, c.type) &&
                IsAllowed(allowedAffinities, c.affinity)
            );
        }
    }

    private bool IsAllowed<T>(T[] list, T value)
    {
        return list.Count() == 0 || list.Contains(value);
    }
}
