using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonRandomDemon : CardAction
{
    public int count = 1;

    public override IEnumerator Execute()
    {
        if (card.Owner.HandCards.Count > 0) {
            var index = Random.Range(0, card.Owner.HandCards.Count);
            Game.SummonDemon(card.Owner.HandCards[index]);
        }
        
        yield return ExecuteNext();
    }
}
