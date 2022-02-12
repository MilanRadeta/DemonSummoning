using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public Player Owner { get; set; }
    public bool IsBlockCard { get; set; }
    public bool FaceUp = false;
    public bool IsMoving { get { return cardTransform.IsMoving; } }
    public Animator Animator { get; private set; }
    public Outline Outline { get; private set; }
    public Vector3 TargetPosition
    {
        get { return cardTransform.TargetPosition; }
        set { cardTransform.TargetPosition = value; }
    }
    public Vector3 TargetRotation
    {
        get { return cardTransform.TargetRotation; }
        set { cardTransform.TargetRotation = value; }
    }

    public CardTransform cardTransform;
    public CardAffinity affinity;
    public CardType type;
    public string cardName;
    [TextArea(5, 5)]
    public string description;
    public List<int> triggerNumbers;
    public CardAction[] actions;

    public delegate void ClickAction(Card card);
    public event ClickAction OnClicked;

    public TMPro.TextMeshPro text;
    public List<TMPro.TextMeshPro> triggerNumberText;
    public MeshRenderer meshRenderer;
    private bool IsUI { get { return gameObject.layer == Layers.UIIndex; } }
    private EventTrigger eventTrigger;

    void Awake()
    {
        Animator = GetComponent<Animator>();
        Outline = GetComponentInChildren<Outline>();
        eventTrigger = GetComponent<EventTrigger>();
    }

    void OnValidate()
    {
        text.text = cardName;
        for (int i = 0; i < triggerNumberText.Count; i++)
        {
            var text = triggerNumberText[i];
            if (i < triggerNumbers.Count)
            {
                var value = triggerNumbers[i];
                text.text = value.ToString();
                text.transform.parent.gameObject.SetActive(true);
                continue;
            }
            text.text = "";
            text.transform.parent.gameObject.SetActive(false);

        }
        for (int i = 0; i < actions.Length - 1; i++)
        {
            actions[i].Next = actions[i + 1];
        }

    }

    void Update()
    {
        if (IsUI)
        {
            Animator.enabled = false;
            Outline.enabled = false;
            eventTrigger.enabled = false;
            return;
        }

        if (Animator.isActiveAndEnabled && FaceUp != Animator.GetBool("FaceUp"))
        {
            Animator.SetBool("FaceUp", FaceUp);
        }

        Outline.enabled = OnClicked != null;
    }

    public void OnPointerClick()
    {
        if (FaceUp && OnClicked != null)
        {
            OnClicked(this);
        }
    }

    public void OnPointerEnter()
    {
        if (FaceUp)
        {
            CardDescription.Instance.Show(this);
        }

    }

    public void OnPointerExit()
    {
        if (FaceUp)
        {
            CardDescription.Instance.Hide();
        }
    }

    public IEnumerator Execute()
    {
        yield return actions[0].ExecuteChain();
    }

    public bool CanExecute(int roll)
    {
        return triggerNumbers.Contains(roll) && (type != CardType.DEMON || Players.Instance.ActivePlayer == Owner);
    }

    public bool CheckCondition()
    {
        var action = actions.ToList().Find(a => a is CheckCondition) as CheckCondition;
        return action == null || action.Execute();
    }
}
