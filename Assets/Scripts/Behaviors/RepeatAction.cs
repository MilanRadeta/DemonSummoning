using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepeatAction : CardAction
{
    public CardAction action;
    public GetCards CardsGetter;

    void OnValidate()
    {
        if (action == null)
        {
            throw new MissingReferenceException("RepeatAction.action not assigned on " + gameObject.name);
        }
        
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("RepeatAction.CardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        var count = CardsGetter.Cards.Count();
        for (int i = 0; i < count; i++)
        {
            yield return action.Execute();
        }
        yield return ExecuteNext();
    }
}
