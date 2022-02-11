using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AwardRollSouls : CardAction
{

    public override bool Execute()
    {
        var rolls = Game.dice.Numbers;
        if (rolls[0] != rolls[1])
        {
            card.Owner.Souls += rolls.Aggregate((acc, x) => acc + x);
        }
        return true;
    }
    
    public override IEnumerator ExecuteChain()
    {
        yield return Game.RollDice();
        yield return base.ExecuteChain();
    }
}
