using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : SingletonBehaviour<GameController>
{
    public List<Card> BlockCards { get { return block.Cards; } }
    public bool IsGameFinished { get { return Players.Instance.IsActivePlayerWinner; } }
    public List<TurnAction> Actions { get; private set; } = new List<TurnAction>();
    public GameConfig config;
    public Block block;
    public Deck blockDeck;
    public Deck demonDeck;
    public Deck discardDeck;
    public Dice dice;
    private TurnAction[] initActions;

    protected override void Start()
    {
        base.Start();
        initActions = GetComponentsInChildren<TurnAction>();
        Players.Instance.Init();
        PlayerSouls.Instance.Init();
        InitDecksAndHands();
        StartCoroutine(ExecuteTurn());
    }

    public IEnumerator ExecuteTurn()
    {
        while (!IsGameFinished)
        {
            yield return new WaitUntil(IsNextTurnPossible);
            Actions = initActions.ToList();
            while (Actions.Count > 0)
            {
                var oldCount = Actions.Count;
                yield return new WaitUntil(() => oldCount != Actions.Count);
            }
            yield return RefillBlock();
            Players.Instance.SwitchToNextPlayer();
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

    public IEnumerator RefillBlock()
    {
        yield return block.Refill(this.blockDeck);
    }

    public void BuyBlockCardForActivePlayer(Card card)
    {
        BuyBlockCard(Players.Instance.ActivePlayer, card);
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

    public IEnumerator RollDice()
    {
        dice.Roll();
        yield return new WaitUntil(() => dice.HasNumbers());
    }

    public bool IsNextTurnPossible()
    {
        return !IsAnythingMoving() && IsBlockFilled();
    }

    public bool IsAnythingMoving()
    {
        return Players.Instance.IsMoving || this.blockDeck.IsMoving || this.discardDeck.IsMoving || this.block.IsMoving;
    }

    private void InitDecksAndHands()
    {
        var cards = CardGenerator.GenerateCards(this.config.cardsConfig.cards, false).OrderBy(c => Random.value).ToList();
        var candles = ExtractCardsOfType(cards, CardType.CANDLE);
        var demons = ExtractCardsOfType(cards, CardType.DEMON);
        Players.Instance.InitCards(candles, demons);
        demonDeck.Init(demons);
        blockDeck.Init(cards);
        block.Init(this);

        StartCoroutine(RefillBlock());
    }

    private Stack<Card> ExtractCardsOfType(List<Card> cards, CardType type)
    {
        var cardsOfType = cards.Where(c => c.type == type).ToList();
        cards.RemoveAll(c => c.type == type);
        return new Stack<Card>(cardsOfType);
    }

    private bool IsBlockFilled()
    {
        return this.block.Cards.Count >= this.config.blockCards;
    }
}
