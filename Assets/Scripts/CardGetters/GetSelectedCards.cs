using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetSelectedCards : GetCards
{
    public SelectCards SelectCardsAction;

    void OnValidate()
    {
        if (SelectCardsAction == null)
        {
            throw new MissingReferenceException("GetSelectedCards.SelectCardsAction not assigned on " + gameObject.name);
        }
    }

    public override IEnumerable<Card> Cards
    {
        get
        {
            return SelectCardsAction.Selected;
        }
    }
}
