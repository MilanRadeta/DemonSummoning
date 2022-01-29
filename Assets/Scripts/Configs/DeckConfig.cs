using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckConfig
{
    [System.Serializable]
    public struct CardCount
    {
        public Card card;
        public int count;
    }

    public CardCount[] cards;
}
