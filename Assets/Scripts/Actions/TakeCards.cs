using System.Collections;

public class TakeCards : CardAction
{
    [NotNull]
    public GetCards CardsGetter;

    public override IEnumerator Execute()
    {
        foreach (var card in CardsGetter.Cards)
        {
            card.Owner.BuyCard(card, 0);
        }
        yield return ExecuteNext();
    }
}
