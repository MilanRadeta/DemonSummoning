using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RefillBlock : CardAction
{

    public override IEnumerator Execute()
    {
        yield return Game.RefillBlock();
        yield return ExecuteNext();
    }
}
