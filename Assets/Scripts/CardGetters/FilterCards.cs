using System.Collections.Generic;
using System.Linq;

public class FilterCards : GetCards
{

    [NotNull]
    public GetCards CardsGetter;
    public CardFilter cardFilter;

    public override IEnumerable<Card> Cards
    {
        get
        {
            return CardsGetter.Cards.Where(c => cardFilter.IsAllowed(c));
        }
    }
}
