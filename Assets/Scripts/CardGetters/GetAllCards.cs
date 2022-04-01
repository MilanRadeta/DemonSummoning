using System.Collections.Generic;
using System.Linq;

public class GetAllCards : GetCards
{
    public override IEnumerable<Card> Cards
    {
        get
        {
            return Players.Instance.AllPlayers.SelectMany(o => o.OpenCards);
        }
    }
}
