using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnchantCard : CardAction
{
    public GetCards CardsGetter;
    public GetEnchantedCards EnchantedCards;

    void OnValidate()
    {
        if (CardsGetter == null)
        {
            throw new MissingReferenceException("EnchantCard.CardsGetter not assigned on " + gameObject.name);
        }
        
        if (EnchantedCards == null)
        {
            throw new MissingReferenceException("EnchantCard.EnchantedCards not assigned on " + gameObject.name);
        }
    }

    public override IEnumerator Execute()
    {
        var cards = CardsGetter.Cards.Where(c => !EnchantedCards.Cards.Contains(c));
        EnchantedCards.EnchantedCards.AddRange(cards);
        yield return ExecuteNext();
    }
}
