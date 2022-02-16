using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExecutePerCard : CardAction
{
    public GetCards CardsGetter;
    public CardAction action;

    void OnValidate()
    {
        if (action == null)
        {
            throw new MissingReferenceException("ExecutePerCard.action not assigned on " + gameObject.name);
        }
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("ExecutePerCard.CardsGetter not assigned on " + gameObject.name);
        }
    }


    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards;
        foreach (var card in cards)
        {
            yield return ExecuteNext();
        }
    }
}
