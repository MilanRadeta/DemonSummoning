using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetNonEnchantedCards : GetCards
{
    public GetCards ValidCards;
    public GetEnchantedCards EnchantedCards;
    
    void OnValidate()
    {
        if (ValidCards == null)
        {
            throw new MissingReferenceException("GetNonEnchantedCards.ValidCards not assigned on " + gameObject.name);
        }
        
        if (EnchantedCards == null)
        {
            throw new MissingReferenceException("GetNonEnchantedCards.EnchantedCards not assigned on " + gameObject.name);
        }
    }

    public override IEnumerable<Card> Cards { get { return ValidCards.Cards.Where(c => !EnchantedCards.Cards.Contains(c)); } }

}
