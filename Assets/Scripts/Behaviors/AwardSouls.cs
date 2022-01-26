using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AwardSouls : CardAction
{
    [Range(1, 10)]
    public int soulsToAward = 1;

    public override bool Execute()
    {
        card.Owner.Souls += soulsToAward;
        return true;
    }
}
