using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepeatAction : CardAction
{
    public CardAction action;
    public GetCards cardsGetter;

    void OnValidate()
    {
        if (action == null)
        {
            throw new MissingReferenceException("RepeatAction.action not assigned on " + gameObject.name);
        }
        
        if (cardsGetter == null)
        {
            throw new MissingReferenceException("RepeatAction.cardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        var count = cardsGetter.Cards.Count();
        for (int i = 0; i < count; i++)
        {
            yield return action.Execute();
        }
        yield return ExecuteNext();
    }
}
