using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardGenerator
{

    public static List<Card> GenerateCards(IEnumerable<DeckConfig.CardCount> config, bool faceUp = true)
    {
        var cards = new List<Card>();
        foreach (var item in config)
        {
            for (int i = 0; i < item.count; i++)
            {
                var card = MonoBehaviour.Instantiate(item.card);
                card.transform.localPosition = Vector3.up * 10;
                card.FaceUp = faceUp;
                cards.Add(card);
                card.gameObject.SetActive(true);
            }
        }
        return cards;
    }

}
