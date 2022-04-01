using System.Collections;

public class RefillBlock : CardAction
{
    public override IEnumerator Execute()
    {
        yield return Game.RefillBlock();
        yield return ExecuteNext();
    }
}
