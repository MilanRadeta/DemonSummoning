using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameConfig config;
    public Players players;

    public Player[] Players { get { return players.AllPlayers; } }
    public List<Card> BlockCards { get { return block.Cards; } }
    public bool IsGameFinished { get { return this.players.IsActivePlayerWinner; } }
    private Player activePlayer { get { return players.ActivePlayer; } }
    public Block block;
    public Deck blockDeck;
    public Deck demonDeck;
    public Deck discardDeck;

    // Start is called before the first frame update
    void Start()
    {
        players.Init(this);
        InitDecksAndHands();
    }

    public void ExecuteTurn()
    {
        while (!IsGameFinished)
        {
            // TODO choose action
            // TODO roll dice mandatory
            // TODO optional demon summon
            // TODO optional card buy
            var roll = RollDice();
            RefillBlock();
            players.SwitchToNextPlayer();
        }
    }

    public Card TakeTopBlockCard()
    {
        return this.blockDeck.TakeTopCard();
    }
    public Card TakeTopDemonCard()
    {
        return this.demonDeck.TakeTopCard();
    }

    public bool CanActivePlayerBuyCard()
    {
        return this.activePlayer.CanBuyCard(this.config.cardPrice);
    }

    public void RefillBlock()
    {
        var missingCards = this.config.blockCards - this.block.Cards.Count;
        for (int i = 0; i < missingCards; i++)
        {
            var card = this.TakeTopBlockCard();
            this.block.AddCard(card);
        }
    }

    public void BuyBlockCard(Player player, Card card)
    {
        this.block.TakeCard(card);
        if (player != null)
        {
            player.BuyCard(card, this.config.cardPrice);
        }
    }

    public void SummonDemon(Card demonCard, IEnumerable<Card> sacrifices = null)
    {
        demonCard.Owner.SummonDemon(demonCard, sacrifices);
    }

    public void Discard(IEnumerable<Card> cards)
    {
        foreach (var card in cards)
        {
            if (card.Owner != null)
            {
                card.Owner.DiscardCards(card);
            }
            else
            {
                this.block.TakeCard(card);
            }
        }
        discardDeck.AddCards(cards);
    }

    public void Discard(Card card)
    {
        Discard(new Card[] { card });
    }

    public int[] RollDice(int numberOfDice = 2, int dieSides = 6)
    {
        return (new int[numberOfDice]).Select(c => Random.Range(0, dieSides) + 1).ToArray();
    }

    private void InitDecksAndHands()
    {
        InitDeck(this.config.demonsConfig, c => c.type == CardType.DEMON, demonDeck);
        InitDeck(this.config.cardsConfig, c => c.type == CardType.CANDLE, blockDeck);
        players.InitCards(this.blockDeck, this.demonDeck);
        InitDeck(this.config.cardsConfig, c => c.type != CardType.CANDLE && c.type != CardType.DEMON, blockDeck, false);

        RefillBlock();
    }

    private void InitDeck(DeckConfig availableCards, System.Func<Card, bool> predicate, Deck deck, bool reset = true)
    {
        deck.Init(availableCards.cards.Where(item => predicate(item.card)), reset);
    }
}
