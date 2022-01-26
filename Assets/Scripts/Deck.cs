using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public Deck alternativeDeck;
    private Stack<Card> cards = new Stack<Card>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
