using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetLastCard : GetCards
{
    public override IEnumerable<Card> Cards { get { return new List<Card>() { card.Owner.OpenCards.Last() }; } }
}
