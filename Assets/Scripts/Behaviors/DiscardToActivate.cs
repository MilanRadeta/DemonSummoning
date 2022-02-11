using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscardToActivate : CardAction
{
    public override bool Execute()
    {
        // TODO Ask to discard
        var answer = false;
        if (answer)
        {
            Game.Discard(card);

        }
        return answer;
    }
}
