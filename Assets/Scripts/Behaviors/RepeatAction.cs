using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepeatAction : CardAction
{
    public CardAction action;
    public GetCards cardsGetter;

    void Start()
    {
        if (action == null)
        {
            Debug.LogError("RepeatAction.action not assigned on " + gameObject.name);
        }
        
        if (cardsGetter == null)
        {
            Debug.LogError("RepeatAction.cardsGetter not assigned on " + gameObject.name);
        }
    }

    public override bool Execute()
    {
        var count = cardsGetter.Cards.Count();
        for (int i = 0; i < count; i++)
        {
            action.Execute();
        }
        return true;
    }
}
