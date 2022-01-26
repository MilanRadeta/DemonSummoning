using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonTopDemon : CardAction
{
    public int count = 1;

    public override bool Execute()
    {
        for (int i = 0; i < count; i++)
        {
            var demon = game.TakeTopDemonCard();
            demon.Owner = card.Owner;
            game.SummonDemon(demon);
        }
        
        return true;
    }
}
