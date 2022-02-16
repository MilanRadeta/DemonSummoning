using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StealSoulPerCard : StealSouls
{
    public GetCards getCards;

    public override IEnumerator Execute()
    {
        var oldSoulsToAward = soulsToAward;
        soulsToAward = getCards.Cards.Count() * soulsToAward;
        yield return base.Execute();
        soulsToAward = oldSoulsToAward;
    }
}
