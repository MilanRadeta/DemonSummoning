using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Souls { get; set; } = 5;
    public List<Card> OpenCards { get { return openCards.ToList(); } }
    public GameObject openCardsObj;
    public GameObject handCardsObj;
    public DeckConfig deckConfig;
    public GameController game;

    private List<Card> openCards = new List<Card>();
    private List<Card> handCards = new List<Card>();

    public void Init(GameController game, int souls)
    {
        this.game = game;
        this.Souls = souls;
    }

    public void BuyCard(Card card, int price)
    {
        Souls -= price;
        openCards.Add(card);
        card.Owner = this;
        card.FaceUp = true;
        RepositionCards();
    }

    public void TakeCard(Card card)
    {
        handCards.Add(card);
        card.Owner = this;
        RepositionCards();
    }

    public void SummonDemon(Card card, IEnumerable<Card> sacrifices)
    {
        if (sacrifices != null && game != null)
        {
            game.Discard(openCards.Where(c => sacrifices.Contains(c)));
        }
        handCards.Remove(card);
        openCards.Add(card);
        card.Owner = this;
        card.FaceUp = true;
        RepositionCards();
    }

    public bool IsWinner(int soulsToWin, int demonsToWin)
    {
        return this.Souls >= soulsToWin && this.openCards.Count(c => c.type == CardType.DEMON) >= demonsToWin;
    }

    public bool CanBuyCard(int price)
    {
        return this.Souls >= price;
    }

    public IEnumerable<Card> DiscardCards(Card card)
    {
        return DiscardCards(c => c == card);
    }

    public IEnumerable<Card> DiscardCards(System.Func<Card, bool> predicate)
    {
        var cards = openCards.Where(c => c.type != CardType.CANDLE).Where(predicate);
        openCards.RemoveAll(c => cards.Contains(c));
        foreach (var card in cards)
        {
            card.Owner = null;
        }
        return cards;
    }

    private void RepositionCards()
    {
        RepositionCards(openCards, openCardsObj);
        RepositionCards(handCards, handCardsObj);
    }
    private void RepositionCards(IEnumerable<Card> cards, GameObject root)
    {
        var length = cards.Count();
        var stepVector = new Vector3(0.5f, -0.01f, 0.125f);
        var halfLength = (length - 1) / 2f;
        var pos = stepVector * (-halfLength);

        foreach (var card in cards)
        {
            card.transform.SetParent(root.transform);
            var rotation = card.transform.localRotation.eulerAngles;
            card.TargetPosition = pos;
            card.TargetRotation = Vector3.zero;
            pos += stepVector;
            if (pos.z >= 0) {
                stepVector.z = -stepVector.z;
            }
        }
    }

}
