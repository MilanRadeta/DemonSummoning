using UnityEngine;

public class PlayerSouls : SingletonBehaviour<PlayerSouls>
{
    public GameObject playerSoulPrefab;
    private GameController game { get { return GameController.Instance; } }

    public void Init()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float posY = 0;
        foreach (var player in Players.Instance.AllPlayers)
        {
            var ui = Instantiate(playerSoulPrefab).GetComponent<PlayerSoul>();
            ui.Init();
            ui.Player = player;
            ui.SetTransform(posY);
            ui.gameObject.SetActive(true);
            posY -= ui.rectTransform.rect.height;
        }
    }
}
