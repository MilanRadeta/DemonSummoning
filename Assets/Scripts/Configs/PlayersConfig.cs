using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayersConfig
{

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

}
