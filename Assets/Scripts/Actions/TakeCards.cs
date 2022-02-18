using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeCards : CardAction
{
    public GetCards CardsGetter;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("TakeCards.cardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        foreach (var card in CardsGetter.Cards)
        {
            card.Owner.BuyCard(card, 0);
        }
        yield return ExecuteNext();
    }
}
