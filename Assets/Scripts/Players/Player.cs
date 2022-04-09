using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Souls = 5;
    public List<Card> OpenCards { get { return openCards.ToList(); } }
    public List<Card> HandCards { get { return handCards.ToList(); } }
    public GameController Game { get { return GameController.Instance; } }
    public Transform openCardsObj;
    public Transform handCardsObj;

    private List<Card> openCards = new List<Card>();
    private List<Card> handCards = new List<Card>();

    public void Init(string name, int souls)
    {
        transform.SetParent(Players.Instance.transform);
        gameObject.name = name;
        Souls = souls;
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
        if (sacrifices != null && Game != null)
        {
            Game.Discard(openCards.Where(c => sacrifices.Contains(c)));
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

    public bool CanSummonDemon(int price)
    {
        return this.handCards.Count > 0 && this.openCards.Count >= price;
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

    public void SetTransform(Transform parent, Vector3 position)
    {
        transform.SetParent(transform);
        transform.localPosition = position;
        transform.LookAt(transform);
    }

    private void RepositionCards()
    {
        RepositionCards(openCards, openCardsObj);
        RepositionCards(handCards, handCardsObj);
    }

    private void RepositionCards(IEnumerable<Card> cards, Transform root)
    {
        var length = cards.Count();
        var stepVector = new Vector3(0.5f, -0.01f, 0.125f);
        var halfLength = (length - 1) / 2f;
        var pos = stepVector * (-halfLength);

        foreach (var card in cards)
        {
            card.SetTransform(root, pos, Vector3.zero);
            pos += stepVector;
            if (pos.z >= 0)
            {
                stepVector.z = -stepVector.z;
            }
        }
    }

}
