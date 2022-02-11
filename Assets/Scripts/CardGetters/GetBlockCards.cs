using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetBlockCards : GetCards
{
    public override IEnumerable<Card> Cards { get { return Game.BlockCards; } }

}
