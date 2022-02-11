using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscardSelected : CardAction
{
    public GetCards getCards;

    void OnValidate()
    {
        if (getCards == null)
        {
            throw new MissingReferenceException("DiscardSelected.selectCardsAction not assigned on " + gameObject.name);
        }
    }

    public override bool Execute()
    {
        var cards = getCards.Cards;
        if (cards == null)
        {
            return false;
        }
        Game.Discard(cards);
        return true;
    }
}
