using UnityEngine;

[System.Serializable]
public class GameConfig
{

    [Range(0, 50)]
    public int demonSacrifices = 3;
    [Range(0, 50)]
    public int cardPrice = 3;

    [Range(1, 10)]
    public int blockCards = 5;

    public DeckConfig cardsConfig;
    public PlayersConfig playersConfig;

}
