using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetOtherPlayerCards : GetCards
{
    public override IEnumerable<Card> Cards { get { return Players.Instance.AllPlayers.Where(p => p != card.Owner).SelectMany(o => o.OpenCards); } }
}
