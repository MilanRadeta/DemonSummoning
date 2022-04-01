using System.Collections.Generic;

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
