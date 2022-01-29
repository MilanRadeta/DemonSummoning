using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Players : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayersConfig config;

    public Player[] AllPlayers { get { return players.Clone() as Player[]; } }
    public bool IsActivePlayerWinner { get { return this.ActivePlayer.IsWinner(this.config.soulsToWin, this.config.demonsToWin); } }
    public Player ActivePlayer { get { return players[activePlayerIndex]; } }

    private Player[] players;
    private int activePlayerIndex = 0;

    public void Init(GameController game)
    {
        if (game != null)
        {
            this.config = game.config.playersConfig;
        }
        
        players = new Player[this.config.numberOfPlayers];
        for (int i = 0; i < this.players.Length; i++)
        {
            var playerObj = Instantiate(playerPrefab);
            this.players[i] = playerObj.GetComponent<Player>();
            this.players[i].Init(game, config.startingSouls);
        }

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
}
