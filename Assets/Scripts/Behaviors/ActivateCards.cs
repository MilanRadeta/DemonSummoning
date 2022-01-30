using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivateCards : CardAction
{
    public GetCards cardsGetter;

    void OnValidate()
    {
        if (cardsGetter == null)
        {
            throw new MissingReferenceException("RepeatAction.cardsGetter not assigned on " + gameObject.name);
        }
    }

    public override bool Execute()
    {
        var cards = cardsGetter.Cards;
        foreach (var card in cards)
        {
            card.Execute();
        }
        return true;
    }
}
