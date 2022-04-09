using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public bool IsMoving { get { return this.cards.ToList().Exists(c => c.IsMoving); } }
    public Deck alternativeDeck;
    private Stack<Card> cards = new Stack<Card>();

    public void Init(IEnumerable<Card> cards)
    {
        this.cards = new Stack<Card>(cards);
        Shuffle();
    }

    public void AddCards(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
        {
            this.cards.Push(card);
        }
        RepositionCards();
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

    private void RepositionCards()
    {
        int y = 0;
        foreach (var card in cards.Reverse())
        {
            var baseVector = Vector3.up * y++;
            card.SetTransform(transform, baseVector * card.meshRenderer.bounds.extents.y, Vector3.zero);
        }
    }
}
