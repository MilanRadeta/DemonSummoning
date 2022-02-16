using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AwardRollSouls : CardAction
{
    
    public override IEnumerator Execute()
    {
        yield return Game.RollDice();
        var rolls = Game.dice.Numbers;
        if (rolls[0] != rolls[1])
        {
            card.Owner.Souls += rolls.Aggregate((acc, x) => acc + x);
        }
        yield return ExecuteNext();
    }
}
