using System.Collections;

public class ActivateCards : CardAction
{
    [NotNull]
    public GetCards CardsGetter;

    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards;
        foreach (var card in cards)
        {
            yield return card.Execute();
        }
        yield return ExecuteNext();
    }
}
