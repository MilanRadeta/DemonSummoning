using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeTop : CardAction
{
    public CardType[] allowedTypes;
    public int count = 1;

    public override bool Execute()
    {
        for (int i = 0; i < count; i++)
        {
            var discard = Game.TakeTopBlockCard();
            if (allowedTypes.Length == 0 || allowedTypes.Contains(discard.type))
            {
                card.Owner.BuyCard(discard, 0);
            }
            else
            {
                Game.Discard(discard);
            }
        }
        return true;
    }
}
