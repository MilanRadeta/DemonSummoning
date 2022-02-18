using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckCondition : CardAction
{
    public CardCondition condition;

    public bool Check()
    {
        return condition.Calculate(card.Owner.OpenCards);
    }

    public override IEnumerator Execute()
    {
        if (Check())
        {
            yield return ExecuteNext();
        }
    }

}
