using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheatGameController : GameController
{

    public Card[] testCards;
    private Card card;
    private List<Card> allCards = new List<Card>();

    private void Choose(Card card)
    {
        this.card = card;
    }

    public override IEnumerator ExecuteTurn()
    {
        while (true)
        {
            allCards.ForEach(c => c.OnClicked += Choose);
            yield return new WaitUntil(() => card != null);
            allCards.ForEach(c => c.OnClicked -= Choose);
            yield return card.Execute();
            card = null;
        }
    }

    protected override void InitDecksAndHands()
    {
        base.InitDecksAndHands();
        var players = Players.Instance.AllPlayers;
        foreach (var player in players)
        {
            foreach (var card in testCards)
            {
                player.BuyCard(Instantiate(card), 0);
            }
            allCards.AddRange(player.OpenCards);
        }
    }
}
