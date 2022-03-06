using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscardToActivate : CardAction
{
    public override IEnumerator Execute()
    {
        Game.Discard(card);
        yield return ExecuteNext();
    }
}
