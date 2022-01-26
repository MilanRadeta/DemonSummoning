using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameConfig config;
    public GameReferences references;

    public Player[] Players { get { return players.Clone() as Player[]; } }
    public List<Card> BlockCards { get { return block.Cards; } }

    public bool IsGameFinished { get { return this.activePlayer.IsWinner(this.config.soulsToWin, this.config.demonsToWin); } }

    private Player[] players;
    private int activePlayerIndex = 0;
    private Player activePlayer { get { return players[activePlayerIndex]; } }
    private Block block;
    private Deck blockDeck;
    private Deck demonDeck;
    private Deck discardDeck;

    // Start is called before the first frame update
    void Start()
    {
        block = this.references.blockObject.GetComponent<Block>();
        blockDeck = this.references.blockDeckObject.GetComponent<Deck>();
        demonDeck = this.references.demonDeckObject.GetComponent<Deck>();
        discardDeck = this.references.discardDeckObject.GetComponent<Deck>();
        InitPlayers();
        InitDecksAndHands();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitPlayers()
    {
        players = new Player[this.config.numberOfPlayers];
        for (int i = 0; i < this.players.Length; i++)
        {
            this.players[i] = new Player();
            this.players[i].Init(this, config.startingSouls);
        }

    }

    void InitDecksAndHands()
    {
        InitDeck(this.config.availableDemons, c => c.type == CardType.DEMON, demonDeck);
        InitDeck(this.config.availableCards, c => c.type == CardType.CANDLE, blockDeck);

        foreach (var player in players)
        {
            player.BuyCard(this.blockDeck.TakeTopCard(), 0);
            for (int i = 0; i < this.config.startingDemons; i++)
            {
                player.TakeCard(this.demonDeck.TakeTopCard());
            }
        }
        InitDeck(this.config.availableCards, c => c.type != CardType.CANDLE && c.type != CardType.DEMON, blockDeck);

        RefillBlock();
    }

    void InitDeck(GameConfig.CardCount[] availableCards, System.Func<Card, bool> predicate, Deck deck)
    {
        var cards = GenerateCards(availableCards, predicate);
        deck.AddCards(cards);
        deck.Shuffle();
    }

    List<Card> GenerateCards(GameConfig.CardCount[] cardCount, System.Func<Card, bool> predicate)
    {
        var cards = new List<Card>();
        foreach (var item in cardCount)
        {
            if (!predicate(item.card))
            {
                continue;
            }

            for (int i = 0; i < item.count; i++)
            {
                var card = Instantiate(item.card);
                card.gameObject.SetActive(false);
                cards.Add(card);
            }
        }
        return cards;
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
            this.activePlayerIndex++;
            this.activePlayerIndex %= this.players.Length;
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
}
