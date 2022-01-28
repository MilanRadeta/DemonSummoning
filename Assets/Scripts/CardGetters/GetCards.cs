using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GetCards : MonoBehaviour
{
    public abstract IEnumerable<Card> Cards { get; }
    protected Card card;
    protected GameController game;

    void Start()
    {
        card = GetComponent<Card>();
        game = FindObjectOfType<GameController>();
    }


}