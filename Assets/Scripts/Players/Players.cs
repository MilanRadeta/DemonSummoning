using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Player playerPrefab;
    public PlayersConfig config;
    public DeckConfig deckConfig;

    public Player[] AllPlayers { get { return players.Clone() as Player[]; } }
    public bool IsActivePlayerWinner { get { return this.ActivePlayer.IsWinner(this.config.soulsToWin, this.config.demonsToWin); } }
    public Player ActivePlayer { get { return players[activePlayerIndex]; } }

    private Player[] players;
    private int activePlayerIndex = 0;

    void OnValidate()
    {
        CardGenerator.RegenerateOnValidate(this, deckConfig?.cards, () =>
        {
            Init();
        });
    }

    public void Init(GameController game = null)
    {
        if (game != null)
        {
            this.config = game.config.playersConfig;
        }
        players = new Player[this.config.numberOfPlayers];
        for (int i = 0; i < this.players.Length; i++)
        {
            var playerObj = Instantiate(playerPrefab);
            playerObj.transform.SetParent(this.transform);
            this.players[i] = playerObj.GetComponent<Player>();
            this.players[i].Init(game, config.startingSouls);
        }
        RepositionPlayers();

    }

    public void InitCards(Deck blockDeck, Deck demonDeck)
    {
        foreach (var player in players)
        {
            player.BuyCard(blockDeck.TakeTopCard(), 0);
            for (int i = 0; i < this.config.startingDemons; i++)
            {
                player.TakeCard(demonDeck.TakeTopCard());
            }
        }
    }

    public void SwitchToNextPlayer()
    {
        this.activePlayerIndex++;
        this.activePlayerIndex %= this.players.Length;
    }

    private void RepositionPlayers()
    {
        var positions = Positioner.PositionAround(this.transform, players.Length, 3);
        var i = 0;
        foreach (var player in players)
        {
            var pos = positions[i++];
            player.transform.SetParent(this.transform);
            player.transform.localPosition = pos;
            player.transform.LookAt(this.transform);
            player.transform.localPosition = pos;
        }
    }
}
