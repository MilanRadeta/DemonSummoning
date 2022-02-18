using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnionCards : GetCards
{

    public GetCards Cards1;
    public GetCards Cards2;

    void OnValidate()
    {
        if (Cards1 == null)
        {
            throw new MissingReferenceException("UnionCards.Cards1 not assigned on " + gameObject.name);
        }
        if (Cards2 == null)
        {
            throw new MissingReferenceException("UnionCards.Cards2 not assigned on " + gameObject.name);
        }
    }

    public override IEnumerable<Card> Cards
    {
        get
        {
            return Cards1.Cards.Concat(Cards2.Cards);
        }
    }
}
