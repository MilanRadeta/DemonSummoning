
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardComponents
{
    public Vector3 TargetPosition { set { CardTransform.TargetPosition = value; } }
    public Vector3 TargetRotation { set { CardTransform.TargetRotation = value; } }
    public Animator Animator { get; private set; }
    public Outline Outline { get; private set; }
    public EventTrigger EventTrigger { get; private set; }
    public CardTransform CardTransform { get; private set; }
    public CardEventHandler EventHandler { get; private set; }
    private Card card;

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
        if (Animator.isActiveAndEnabled && card.FaceUp != Animator.GetBool("FaceUp"))
        {
            Animator.SetBool("FaceUp", card.FaceUp);
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