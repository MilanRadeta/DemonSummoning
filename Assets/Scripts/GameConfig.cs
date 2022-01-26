using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameConfig
{

    [System.Serializable]
    public struct CardCount {
        public Card card;
        public int count;
    }

    [Range(1, 5)]
    public int numberOfPlayers = 2;

    [Range(0, 100)]
    public int soulsToWin = 10;

    [Range(0, 10)]
    public int demonsToWin = 3;

    [Range(0, 100)]
    public int startingSouls = 5;

    [Range(0, 10)]
    public int startingDemons = 5;
    
    [Range(0, 50)]
    public int cardPrice = 3;
    
    [Range(1, 10)]
    public int blockCards = 5;

    public CardCount[] availableCards;

    public CardCount[] availableDemons;
    
}
