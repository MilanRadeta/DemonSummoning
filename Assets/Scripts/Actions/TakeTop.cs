using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeTop : CardAction
{
    public CardFilter cardFilter;
    public int count = 1;

    public override IEnumerator Execute()
    {
        for (int i = 0; i < count; i++)
        {
            var discard = Game.TakeTopBlockCard();
            if (cardFilter.IsAllowed(discard))
            {
                card.Owner.BuyCard(discard, 0);
            }
            else
            {
                Game.Discard(discard);
            }
        }
        yield return ExecuteNext();
    }
}
