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
    public CardAction startingAction;

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

        if (!IsUI)
        {
            if (ReferenceEquals(startingAction, null))
            {
                Debug.Log("Missing action on card " + name + "!");
            }
            else
            {
                var allActions = GetComponents<CardAction>().ToList();
                var perCardActions = GetComponents<ExecutePerCard>().ToList();
                var action = startingAction;
                do {
                    allActions.Remove(action);
                    action = action.Next;
                } while (action != null);

                foreach (var perCardAction in perCardActions)
                {
                    allActions.Remove(perCardAction.action);
                }
                
                if (allActions.Count > 0) {
                    Debug.Log("Unused actions on card " + name + ": " + string.Join(",", allActions.Select(c => c.GetType())));
                }
            }
        }
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
        yield return startingAction.Execute();
    }

    public bool CanExecute(int roll)
    {
        return triggerNumbers.Contains(roll) && (type != CardType.DEMON || Players.Instance.ActivePlayer == Owner);
    }

    public bool CheckCondition()
    {
        if (startingAction is CheckCondition)
        {
            return (startingAction as CheckCondition).Check();
        }
        return true;
    }
}
