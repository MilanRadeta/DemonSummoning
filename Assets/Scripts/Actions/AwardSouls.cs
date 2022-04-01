using System.Collections;
using UnityEngine;

public class AwardSouls : CardAction
{
    [Range(1, 10)]
    public int soulsToAward = 1;

    public override IEnumerator Execute()
    {
        card.Owner.Souls += soulsToAward;
        yield return ExecuteNext();
    }
}
