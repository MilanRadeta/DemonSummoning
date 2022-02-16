using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AwardSoulsToPlayer : CardAction
{
    [Range(1, 10)]
    public int soulsToAward = 1;

    public override IEnumerator Execute()
    {
        var otherPlayers = Players.Instance.GetOtherPlayers(card.Owner);
        Player player = null; // TODO choose player
        if (player != null)
        {
            player.Souls += soulsToAward;
        }
        yield return ExecuteNext();
    }
}
