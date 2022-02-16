using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonTopDemon : CardAction
{
    public int count = 1;

    public override IEnumerator Execute()
    {
        for (int i = 0; i < count; i++)
        {
            var demon = Game.TakeTopDemonCard();
            demon.Owner = card.Owner;
            Game.SummonDemon(demon);
        }
        
        yield return ExecuteNext();
    }
}
