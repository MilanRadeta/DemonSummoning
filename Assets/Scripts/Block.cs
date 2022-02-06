using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int blockCards = 5;
    public List<Card> Cards { get { return cards.Where(c => c != null).ToList(); } }
    public bool IsMoving { get { return Cards.Exists(c => c.IsMoving); } }
    private List<Card> cards = new List<Card>();

    public void Init(GameController game) {
        blockCards = game.config.blockCards;
    }

    public void AddCard(Card card)
    {
        card.IsBlockCard = true;
        card.FaceUp = true;
        var addNew = true;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                addNew = false;
                cards[i] = card;
                break;
            }
        }
        if (addNew)
        {
            this.cards.Add(card);
        }
        RepositionCards();
    }

    public void TakeCard(Card card)
    {
        card.IsBlockCard = false;
        var index = this.cards.IndexOf(card);
        this.cards[index] = null;
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

    public IEnumerator Refill(Deck deck)
    {
        yield return new WaitUntil(() => !deck.IsMoving);
        var missingCards = blockCards - Cards.Count;
        for (int i = 0; i < missingCards; i++)
        {
            var card = deck.TakeTopCard();
            AddCard(card);
        }
    }


}
