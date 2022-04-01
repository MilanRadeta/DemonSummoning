using System.Collections.Generic;

public class GetOpenCards : GetCards
{
    public override IEnumerable<Card> Cards { get { return card.Owner.OpenCards; } }

}
