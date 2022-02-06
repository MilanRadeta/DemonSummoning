using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextTurnAction : TurnAction
{

    public override bool CanExecute()
    {
        return base.CanExecute() && !Game.Actions.Any(a => a is RollDiceAction);
    }

    public override IEnumerator Execute()
    {
        Game.Actions.Clear();
        yield return base.Execute();
    }

}
