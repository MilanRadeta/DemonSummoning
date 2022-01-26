using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CardAction : MonoBehaviour
{
    protected Card card;
    protected GameController game;

    void Start()
    {
        card = GetComponent<Card>();
        game = FindObjectOfType<GameController>();
    }

    public abstract bool Execute();
}
