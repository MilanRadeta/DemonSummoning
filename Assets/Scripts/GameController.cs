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
    public Dice dice;

    void Start()
    {
        players.Init(this);
        InitDecksAndHands();
        StartCoroutine(ExecuteTurn());
    }

    public IEnumerator ExecuteTurn()
    {
        while (!IsGameFinished)
        {
            yield return new WaitUntil(() => !this.blockDeck.IsMoving && !this.discardDeck.IsMoving && !this.block.IsMoving && this.block.Cards.Count >= this.config.blockCards);
            // TODO choose action
            // TODO roll dice mandatory
            // TODO optional demon summon
            // TODO optional card buy
            RollDice();
            yield return new WaitUntil(() => dice.HasNumbers());
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

    public IEnumerator RefillBlock()
    {
        yield return new WaitUntil(() => !this.blockDeck.IsMoving);
        var missingCards = this.config.blockCards - this.block.Cards.Count;
        for (int i = 0; i < missingCards; i++)
        {
            var card = this.TakeTopBlockCard();
            this.block.AddCard(card);
        }
    }

    public void BuyBlockCardForActivePlayer(Card card)
    {
        BuyBlockCard(activePlayer, card);
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

    public void RollDice()
    {
        dice.Roll();
    }

    private void InitDecksAndHands()
    {
        var cards = CardGenerator.GenerateCards(this.config.cardsConfig.cards, false).OrderBy(c => Random.value).ToList();
        var candles = ExtractCardsOfType(cards, CardType.CANDLE);
        var demons = ExtractCardsOfType(cards, CardType.DEMON);
        players.InitCards(candles, demons);
        cards.AddRange(candles);
        demonDeck.Init(demons);
        blockDeck.Init(cards);

        StartCoroutine(RefillBlock());
    }

    private Stack<Card> ExtractCardsOfType(List<Card> cards, CardType type)
    {
        var cardsOfType = cards.Where(c => c.type == type).ToList();
        cards.RemoveAll(c => c.type == type);
        return new Stack<Card>(cardsOfType);
    }
}
