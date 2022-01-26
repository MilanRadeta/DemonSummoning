using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RefillBlock : CardAction
{

    public override bool Execute()
    {
        game.RefillBlock();
        return true;
    }
}
