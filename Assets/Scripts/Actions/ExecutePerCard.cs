using System.Collections;

public class ExecutePerCard : CardAction
{
    [NotNull]
    public GetCards CardsGetter;
    [NotNull]
    public CardAction action;

    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards;
        foreach (var card in cards)
        {
            yield return ExecuteNext();
        }
    }
}
