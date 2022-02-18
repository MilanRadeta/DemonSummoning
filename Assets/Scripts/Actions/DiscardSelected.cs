using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscardSelected : CardAction
{
    public GetCards CardsGetter;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("DiscardSelected.CardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards;
        if (cards != null)
        {
            Game.Discard(cards);
            yield return ExecuteNext();
        }
    }
}
