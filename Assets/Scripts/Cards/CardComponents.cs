
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardComponents
{
    public Animator Animator { get; private set; }

    public Outline Outline { get; private set; }

    [NonSerialized]
    public EventTrigger EventTrigger;
    private Card card;

    public CardComponents(Card card)
    {
        this.card = card;
        Animator = card.GetComponent<Animator>();
        Outline = card.GetComponentInChildren<Outline>();
        EventTrigger = card.GetComponent<EventTrigger>();
    }

    public void Update()
    {
        if (Animator.isActiveAndEnabled && card.FaceUp != Animator.GetBool("FaceUp"))
        {
            Animator.SetBool("FaceUp", card.FaceUp);
        }

        Outline.enabled = card.Clickable;
        var colors = card.outlineColors;
        Outline.OutlineColor = card.Selected ? colors.deselectable : colors.selectable;
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
    

}