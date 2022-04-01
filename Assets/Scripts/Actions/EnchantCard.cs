using System.Collections;
using System.Linq;

public class EnchantCard : CardAction
{
    [NotNull]
    public GetCards CardsGetter;
    [NotNull]
    public GetEnchantedCards EnchantedCards;

    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards.Where(c => !EnchantedCards.Cards.Contains(c));
        EnchantedCards.EnchantedCards.AddRange(cards);
        yield return ExecuteNext();
    }
}
