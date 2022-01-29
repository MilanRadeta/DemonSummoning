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

    public void Init(IEnumerable<DeckConfig.CardCount> cardsConfig = null)
    {
        Reset();
        if (cardsConfig == null)
        {
            cardsConfig = config.cards;
        }
        cards = CardGenerator.GenerateCards(cardsConfig);
        RepositionCards();
    }

    public void AddCard(Card card)
    {
        this.cards.Add(card);
    }

    public void TakeCard(Card card)
    {
        this.cards.Remove(card);
    }

    private void Reset()
    {
        cards = new List<Card>();
        CardGenerator.Delete(this);
    }

    private void RepositionCards()
    {
        if (cards.Count > 0)
        {
            int y = 0;
            var delta = 360 / cards.Count;
            foreach (var card in cards)
            {
                card.transform.SetParent(gameObject.transform);
                card.transform.localPosition = Vector3.forward;
                card.transform.RotateAround(transform.position, Vector3.up, delta * y++);
            }
        }
    }


}
