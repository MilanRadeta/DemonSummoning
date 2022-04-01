using System.Collections;

public class DiscardToActivate : CardAction
{
    public override IEnumerator Execute()
    {
        Game.Discard(card);
        yield return ExecuteNext();
    }
}
