using System.Collections.Generic;
using System.Linq;

public class GetOtherPlayerCards : GetCards
{
    public override IEnumerable<Card> Cards
    {
        get
        {
            return Players.Instance
                .GetOtherPlayers(card.Owner)
                .SelectMany(o => o.OpenCards);
        }
    }
}
