using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollDiceAction : TurnAction
{
    
    public override IEnumerator Execute()
    {
        yield return Game.RollDice();
        var cards = Players.Instance.AllPlayers.SelectMany(p => p.OpenCards).Where(p => p.type != CardType.DEMON || Players.Instance.ActivePlayer == p);
        yield return base.Execute();
    }

}
