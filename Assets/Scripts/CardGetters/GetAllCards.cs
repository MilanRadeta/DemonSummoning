using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetAllCards : GetCards
{
    public override IEnumerable<Card> Cards { get { return Players.Instance.AllPlayers.SelectMany(o => o.OpenCards); } }
}
