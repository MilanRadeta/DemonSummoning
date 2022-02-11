using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            ui.transform.SetParent(this.transform);
            ui.Player = player;
            var transform = ui.GetComponent<RectTransform>();
            transform.localScale = Vector3.one;
            transform.localEulerAngles = Vector3.zero;
            transform.anchoredPosition3D = new Vector3(0, posY, 0);
            transform.offsetMin = new Vector2(0, transform.offsetMin.y);
            transform.offsetMax = new Vector2(0, transform.offsetMax.y);
            posY -= transform.rect.height;
            ui.gameObject.SetActive(true);
        }
    }
}
