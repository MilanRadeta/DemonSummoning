using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollDiceAction : TurnAction
{
    
    public override IEnumerator Execute()
    {
        yield return Game.RollDice();
        yield return base.Execute();
    }

}
