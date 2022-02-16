using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscardToActivate : CardAction
{
    public override IEnumerator Execute()
    {
        // TODO Ask to discard
        var answer = false;
        if (answer)
        {
            Game.Discard(card);
            yield return ExecuteNext();
        }
    }
}
