using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StealSouls : AwardSouls
{

    public override IEnumerator Execute()
    {
        var otherPlayers = Players.Instance.GetOtherPlayers(card.Owner);
        for (int i = 0; i < soulsToAward; i++)
        {
            otherPlayers = otherPlayers.Where(p => p.Souls > 0);
            Player player = null; // TODO choose player's stack
            if (player == null) 
            {
                break;
            }
        }
        yield return base.Execute();
    }
}
