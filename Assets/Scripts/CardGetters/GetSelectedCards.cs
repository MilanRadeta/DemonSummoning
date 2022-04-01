using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetSelectedCards : GetCards
{
    [NotNull]
    public SelectCards SelectCardsAction;
    
    public override IEnumerable<Card> Cards
    {
        get
        {
            return SelectCardsAction.Selected;
        }
    }
}
