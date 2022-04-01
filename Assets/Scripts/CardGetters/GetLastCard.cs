using System.Collections.Generic;
using System.Linq;

public class GetLastCard : GetCards
{
    public override IEnumerable<Card> Cards
    {
        get
        {
            return new List<Card>() { card.Owner.OpenCards.Last() };
        }
    }
}
