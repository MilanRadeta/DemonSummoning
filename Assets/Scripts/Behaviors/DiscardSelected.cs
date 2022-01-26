using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscardSelected : CardAction
{
    public GetCards getCards;

    void Start()
    {
        if (getCards == null)
        {
            Debug.LogError("DiscardSelected.selectCardsAction not assigned on " + gameObject.name);
        }
    }

    public override bool Execute()
    {
        var cards = getCards.Cards;
        if (cards == null)
        {
            return false;
        }
        game.Discard(cards);
        return true;
    }
}
