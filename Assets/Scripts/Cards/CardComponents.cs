
using UnityEngine;
using UnityEngine.EventSystems;

public class CardComponents
{
    public readonly Animator Animator;
    public readonly EventTrigger EventTrigger;
    public readonly CardTransform CardTransform;
    public readonly CardEventHandler EventHandler;
    private readonly Card card;
    private readonly string FACE_UP_KEY = "FaceUp";

    public CardComponents(Card card)
    {
        this.card = card;
        Animator = card.GetComponent<Animator>();
        EventTrigger = card.GetComponent<EventTrigger>();
        CardTransform = card.GetComponent<CardTransform>();
        EventHandler = card.GetComponent<CardEventHandler>();
    }

    public void Update()
    {
        if (Animator.isActiveAndEnabled && card.FaceUp != Animator.GetBool(FACE_UP_KEY))
        {
            Animator.SetBool(FACE_UP_KEY, card.FaceUp);
        }
    }

}