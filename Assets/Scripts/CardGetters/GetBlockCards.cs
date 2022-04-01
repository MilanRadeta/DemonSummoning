using System.Collections.Generic;

public class GetBlockCards : GetCards
{
    public override IEnumerable<Card> Cards
    {
        get
        {
            return Game.BlockCards;
        }
    }

}
