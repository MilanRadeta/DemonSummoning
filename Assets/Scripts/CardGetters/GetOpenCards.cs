using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetOpenCards : GetCards
{
    public override IEnumerable<Card> Cards { get { return card.Owner.OpenCards; } }

}
