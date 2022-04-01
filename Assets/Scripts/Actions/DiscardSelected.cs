using System.Collections;

public class DiscardSelected : CardAction
{
    [NotNull]
    public GetCards CardsGetter;

    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards;
        if (cards != null)
        {
            Game.Discard(cards);
            yield return ExecuteNext();
        }
    }
}
