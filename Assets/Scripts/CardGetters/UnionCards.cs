using System.Collections.Generic;
using System.Linq;

public class UnionCards : GetCards
{

    [NotNull]
    public GetCards Cards1;
    [NotNull]
    public GetCards Cards2;

    public override IEnumerable<Card> Cards
    {
        get
        {
            return Cards1.Cards.Concat(Cards2.Cards);
        }
    }
}
