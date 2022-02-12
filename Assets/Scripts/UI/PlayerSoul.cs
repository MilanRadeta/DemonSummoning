using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSoul : MonoBehaviour
{

    public Player Player
    {
        get { return player; }
        set
        {
            player = value;
            souls = player.Souls;
            nameText.text = player.name;
            soulsText.text = soulsPrefix + player.Souls;
        }
    }
    public Text nameText;
    public Text soulsText;
    public string soulsPrefix;
    private readonly float textChangeSpeedInSeconds = 0.25f;
    private Player player;
    private int souls;
    private Image image;


    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (player.Souls != souls)
        {
            souls += Mathf.Max(1, Mathf.RoundToInt((player.Souls - souls) * Time.deltaTime / textChangeSpeedInSeconds));
            soulsText.text = soulsPrefix + (int)souls;
        }
        image.color = player == Players.Instance.ActivePlayer ? Color.green : Color.white;
        
    }

}
