using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeTop : CardAction
{
    public CardType[] allowedTypes;
    public CardAffinity[] allowedAffinities;
    public int count = 1;

    public override IEnumerator Execute()
    {
        for (int i = 0; i < count; i++)
        {
            var discard = Game.TakeTopBlockCard();
            if (IsAllowed(allowedTypes, discard.type) && IsAllowed(allowedAffinities, discard.affinity))
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

    private bool IsAllowed<T>(T[] list, T value)
    {
        return list.Count() == 0 || list.Contains(value);
    }
}
