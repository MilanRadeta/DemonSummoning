using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckCondition : CardAction
{
    public CardCondition condition;

    public override bool Execute()
    {
        return condition.Calculate(card.Owner.OpenCards);
    }

}
