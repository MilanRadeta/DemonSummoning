using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    public List<Card> Cards { get { return cards.ToList(); } }
    private List<Card> cards = new List<Card>();
    public bool IsMoving { get { return this.cards.ToList().Exists(c => c.IsMoving); } }

    public void AddCard(Card card)
    {
        card.IsBlockCard = true;
        card.FaceUp = true;
        this.cards.Add(card);
        RepositionCards();
    }

    public void TakeCard(Card card)
    {
        card.IsBlockCard = false;
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


}
