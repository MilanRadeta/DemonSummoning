using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivateCards : CardAction
{
    public GetCards CardsGetter;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("ActivateCards.CardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards;
        foreach (var card in cards)
        {
            yield return card.Execute();
        }
        yield return ExecuteNext();
    }
}
