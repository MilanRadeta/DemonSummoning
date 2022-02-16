using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetEnchantedCards : GetCards
{
    public override IEnumerable<Card> Cards { get { return EnchantedCards; } }

    public List<Card> EnchantedCards { get; set; } = new List<Card>();

}
