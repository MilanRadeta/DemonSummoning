using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StealSoulPerCard : StealSouls
{
    public GetCards CardsGetter;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("StealSoulPerCard.CardsGetter not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        var oldSoulsToAward = soulsToAward;
        soulsToAward = CardsGetter.Cards.Count() * soulsToAward;
        yield return base.Execute();
        soulsToAward = oldSoulsToAward;
    }
}
