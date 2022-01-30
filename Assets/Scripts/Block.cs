using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<Card> Cards { get { return cards.ToList(); } }
    public DeckConfig config;
    private List<Card> cards = new List<Card>();

    void OnValidate()
    {
        CardGenerator.RegenerateOnValidate(this, config?.cards, () => Init());
    }

    public void AddCard(Card card)
    {
        this.cards.Add(card);
    }

    public void TakeCard(Card card)
    {
        this.cards.Remove(card);
    }

    private void RepositionCards()
    {
        var positions = Positioner.PositionAround(this.transform, cards.Count);
        var i = 0;
        foreach (var card in cards)
        {
            card.transform.SetParent(this.transform);
            card.TargetPosition = positions[i++];
        }
    }

    private void Init(IEnumerable<DeckConfig.CardCount> cardsConfig = null)
    {
        if (this == null)
        {
            return;
        }
        if (cardsConfig == null)
        {
            cardsConfig = config.cards;
        }
        cards = CardGenerator.GenerateCards(cardsConfig, true);
        RepositionCards();
    }


}
