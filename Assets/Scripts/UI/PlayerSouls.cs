using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSouls : SingletonBehaviour<PlayerSouls>
{
    public GameObject playerSoulPrefab;
    private GameController game { get { return GameController.Instance; } }

}
