using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public DeckConfig config;
    public Deck alternativeDeck;
    public bool faceUp = false;
    private Stack<Card> cards = new Stack<Card>();

    void OnValidate()
    {
        CardGenerator.RegenerateOnValidate(this, config?.cards, () => Init());
    }

    public void Init(IEnumerable<DeckConfig.CardCount> cardsConfig = null, bool reset = true)
    {
        if (reset) {
            Reset();
        }
        if (cardsConfig == null)
        {
            cardsConfig = config.cards;
        }
        var cards = new Stack<Card>(CardGenerator.GenerateCards(cardsConfig, faceUp));
        AddCards(cards);
        Shuffle();
    }

    public void AddCards(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
        {
            this.cards.Push(card);
        }
    }

    public void Shuffle()
    {
        this.cards = new Stack<Card>(this.cards.OrderBy(x => Random.value));
        RepositionCards();
    }

    public void SwapDecks(Deck deck)
    {
        var temp = this.cards;
        this.cards = deck.cards;
        deck.cards = temp;
    }

    public bool HasNext()
    {
        return this.cards.Count > 0;
    }

    public Card TakeTopCard()
    {
        if (!this.HasNext())
        {
            if (alternativeDeck != null)
            {
                alternativeDeck.SwapDecks(this);
                this.Shuffle();
            }
        }
        return this.cards.Pop();
    }

    private void Reset()
    {
        cards = new Stack<Card>();
        CardGenerator.Delete(this);
    }

    private void RepositionCards()
    {
        int y = 0;
        foreach (var card in cards.Reverse())
        {
            card.transform.SetParent(gameObject.transform);
            card.transform.localPosition = Vector3.up * card.meshRenderer.bounds.extents.y * y++;
        }
    }
}
