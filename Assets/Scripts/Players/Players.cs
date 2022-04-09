using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Players : SingletonBehaviour<Players>
{
    public Player playerPrefab;
    public Rotator rotator;

    public bool IsMoving { get { return rotator.IsMoving; } }
    public Player[] AllPlayers { get { return players.Clone() as Player[]; } }
    public bool IsActivePlayerWinner { get { return this.ActivePlayer.IsWinner(this.config.soulsToWin, this.config.demonsToWin); } }
    public Player ActivePlayer { get { return players[activePlayerIndex]; } }

    private Player[] players;
    private int activePlayerIndex = 0;
    private PlayersConfig config;

    public void Init()
    {
        this.config = GameController.Instance.config.playersConfig;
        players = new Player[this.config.numberOfPlayers];
        for (int i = 0; i < this.players.Length; i++)
        {
            var playerObj = Instantiate(playerPrefab);
            this.players[i] = playerObj.GetComponent<Player>();
            this.players[i].Init("Player " + (i + 1), config.startingSouls);
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

    public IEnumerable<Player> GetPlayersFromActivePlayer()
    {
        var players = this.players.ToList();
        return players
            .GetRange(activePlayerIndex, players.Count - activePlayerIndex)
            .Concat(players.GetRange(0, activePlayerIndex));
    }

    public IEnumerable<Player> GetOtherPlayers(Player ignore)
    {
        return players.Where(p => p != ignore);
    }

    private void RepositionPlayers()
    {
        var positions = Positioner.PositionAround(transform, players.Length, 3);
        var i = 0;
        foreach (var player in players)
        {
            player.SetTransform(transform, positions[i++]);
        }
    }
}
