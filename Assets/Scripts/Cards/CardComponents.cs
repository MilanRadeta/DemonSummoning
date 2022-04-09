
using UnityEngine;
using UnityEngine.EventSystems;

public class CardComponents
{
    public readonly Animator Animator;
    public readonly Outline Outline;
    public readonly EventTrigger EventTrigger;
    public readonly CardTransform CardTransform;
    public readonly CardEventHandler EventHandler;
    private readonly Card card;
    private readonly string FACE_UP_KEY = "FaceUp";

    public CardComponents(Card card)
    {
        this.card = card;
        Animator = card.GetComponent<Animator>();
        Outline = card.GetComponentInChildren<Outline>();
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

        Outline.enabled = card.IsClickable;
        var colors = card.outlineColors;
        Outline.OutlineColor = card.IsSelected ? colors.deselectable : colors.selectable;
    }

    private void Enable(bool value = true)
    {
        Animator.enabled = value;
        Outline.enabled = value;
        EventTrigger.enabled = value;
    }

    public void Disable(bool value = true)
    {
        Enable(!value);
    }

    public void Rotate(Vector3 value)
    {
        CardTransform.TargetRotation = value;
    }


}