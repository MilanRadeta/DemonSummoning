using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Players : SingletonBehaviour<Players>
{
    public Player playerPrefab;
    public DeckConfig deckConfig;
    public GameController game;
    public Rotator rotator;

    public bool IsMoving { get { return rotator.IsMoving; } }
    public Player[] AllPlayers { get { return players.Clone() as Player[]; } }
    public bool IsActivePlayerWinner { get { return this.ActivePlayer.IsWinner(this.config.soulsToWin, this.config.demonsToWin); } }
    public Player ActivePlayer { get { return players[activePlayerIndex]; } }

    private Player[] players;
    private int activePlayerIndex = 0;
    private PlayersConfig config;

    public void Init(GameController game)
    {
        this.config = game.config.playersConfig;
        players = new Player[this.config.numberOfPlayers];
        for (int i = 0; i < this.players.Length; i++)
        {
            var playerObj = Instantiate(playerPrefab);
            playerObj.transform.SetParent(this.transform);
            this.players[i] = playerObj.GetComponent<Player>();
            this.players[i].Init(config.startingSouls);
        }
        RepositionPlayers();

    }

    public void InitCards(Stack<Card> candles, Stack<Card> demons)
    {
        foreach (var player in players)
        {
            player.BuyCard(candles.Pop(), 0);
            for (int i = 0; i < this.config.startingDemons; i++)
            {
                player.TakeCard(demons.Pop());
            }
        }
    }

    public void SwitchToNextPlayer()
    {
        this.activePlayerIndex++;
        this.activePlayerIndex %= this.players.Length;
        var step = 360 / this.players.Length;
        rotator.Target = new Vector3(0, -this.activePlayerIndex * step, 0);
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
