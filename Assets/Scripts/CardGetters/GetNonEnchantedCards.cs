using System.Collections.Generic;
using System.Linq;

public class GetNonEnchantedCards : GetCards
{
    [NotNull]
    public GetCards ValidCards;
    [NotNull]
    public GetEnchantedCards EnchantedCards;
    
    public override IEnumerable<Card> Cards { get { return ValidCards.Cards.Where(c => !EnchantedCards.Cards.Contains(c)); } }

}
