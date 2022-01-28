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
        if (config.cards.Length > 0)
        {

            UnityEditor.EditorApplication.delayCall += () =>
              {
                  if (this == null)
                  {
                      return;
                  }
                  Init();
              };
        };
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && cards.Count > 0)
        {
            var card = cards.Pop();
            card.FlipUp();
            
            if (alternativeDeck != null)
            {
                card.transform.SetParent(alternativeDeck.transform);
            }
            card.MoveToParent();
        }
    }

    public void Init(IEnumerable<DeckConfig.CardCount> cardsConfig = null)
    {
        Reset();
        if (cardsConfig == null)
        {
            cardsConfig = config.cards;
        }
        var cards = GenerateCards();
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
        while (transform.childCount > 0)
        {
            var child = transform.GetChild(0).gameObject;
            if (Application.isEditor)
            {
                DestroyImmediate(child);
            }
            else
            {
                Destroy(child);
            }
        }
    }

    private List<Card> GenerateCards()
    {
        var cards = new List<Card>();
        foreach (var item in config.cards)
        {
            for (int i = 0; i < item.count; i++)
            {
                var card = Instantiate(item.card);
                cards.Add(card);
                card.gameObject.SetActive(true);
            }
        }
        return cards;
    }

    private void RepositionCards()
    {
        int y = 0;
        foreach (var card in cards.Reverse())
        {

            card.transform.SetParent(gameObject.transform);
            card.transform.localPosition = Vector3.up * card.meshRenderer.bounds.extents.y * y++;
            card.transform.localRotation = Quaternion.Euler(new Vector3(1, 1, 0) * (faceUp ? 1 : 180));
        }
    }
}
